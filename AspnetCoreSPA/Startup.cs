using AspnetCoreSPATemplate.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AspnetCoreSPATemplate.Services.Common;
using SqlServerDataAccess;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using SqlServerDataAccess.EF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Common.Identity;

namespace AspnetCoreSPATemplate
{
    public class Startup
    {
        private JwtConfiguration _jwtConfig;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            // Configure authentication
            _jwtConfig = Configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>();
            services.Configure<JwtConfiguration>(Configuration.GetSection("JwtConfiguration"));
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = _jwtConfig.Issuer,
                        ValidAudience = _jwtConfig.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key))
                    };
                });

            // Configure database
            if (Configuration["DataSource"] == "SqlServer")
            {
                // Auto Mapper
                services.AddAutoMapper(typeof(Startup));

                // Fetching Ccnnection string from appsettings.json
                string connectionString = Configuration.GetConnectionString("DbConstr");
                // Entity Framework
                services.AddDbContext<ContactsMgmtContext>(options => options.UseSqlServer(connectionString));
                services.AddDbContext<ContactsMgmtIdentityContext>(options => options.UseSqlServer(connectionString));
            }

            //services.AddTransient<IContactRepository, TestContactRepository>();
            //services.AddTransient<IContactRepository, CsvContactRepository>();
            //services.AddTransient<IContactRepository, CsvHelperContactRepository>();
            services.AddTransient<IContactRepository, SqlServerContactRepository>();
            services.AddTransient<IContactModificationRepository, SqlServerContactRepository>();
            services.AddTransient<IAuthenticationRepository, SqlServerAuthenticationRepository>();
            services.AddTransient<IUserRepository, SqlServerUserRepository>();
            services.AddTransient<IUserModificationRepository, SqlServerUserRepository>();

            // Configure identity
            services
                .AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                })
                .AddEntityFrameworkStores<ContactsMgmtIdentityContext>()
                .AddDefaultTokenProviders();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // In production, the SPA files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "app/dist";
            });

            ConfigureAuthorization(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "app";

                if (env.IsDevelopment())
                {
                    //spa.UseProxyToSpaDevelopmentServer("http://localhost:8080");
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }

        private static void ConfigureAuthorization(IServiceCollection services)
        {
            services.AddAuthorization(options => {
                options.AddPolicy("RequireStandard", policy => policy.RequireRole("Standard", "Manager", "HR", "Admin"));
                options.AddPolicy("RequireManager", policy => policy.RequireRole("Manager", "Admin"));
                options.AddPolicy("RequireHR", policy => policy.RequireRole("HR", "Admin"));
                options.AddPolicy("RequireAdmin", policy => policy.RequireRole("Admin"));
            });
        }
    }
}
