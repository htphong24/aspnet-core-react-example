using System;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common.Configuration;
using Common.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Services;
using SqlServerDataAccess.EF;
// ReSharper disable CheckNamespace

namespace AspnetCoreSPATemplate
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var jwtConfig = config.GetSection("Jwt").Get<JwtConfiguration>();
            services.Configure<JwtConfiguration>(config.GetSection("Jwt"));
            services.Configure<AuthenticationConfiguration>(config.GetSection("Authentication"));
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            // Automatically add Bearer token to each coming request
                            // So we don't need to store the access token on client side (e.g. localStorage)
                            // Do this either in JwtBearerEvent.OnMessageReceive or app.Use (under Startup > Configure)
                            context.Token = context.Request.Cookies["accessToken"];
                            return Task.CompletedTask;
                        }
                    };
                    //options.RequireHttpsMetadata = false; // disabled in development
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtConfig.Issuer,
                        ValidAudience = jwtConfig.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key)),
                        ClockSkew = TimeSpan.Zero // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    };
                });

            return services;
        }

        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration config)
        {
            if (config["DataSource"] != "SqlServer")
                return services;

            // Fetching Connection string from appsettings.json
            var connectionString = config.GetConnectionString("DbConstr");
            // Entity Framework
            services.AddDbContext<ContactsMgmtContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<ContactsMgmtIdentityContext>(options => options.UseSqlServer(connectionString));

            return services;
        }

        public static IServiceCollection ConfigureOtherServices(this IServiceCollection services)
        {
            // AutoMapper
            var mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ContactProfile());
                mc.AddProfile(new UserProfile());
            }).CreateMapper();

            services.AddSingleton(mapper);
            //services.AddAutoMapper(typeof(Startup));

            return services;
        }

        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            services
                .AddIdentityCore<ApplicationUser>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;

                    options.Lockout.MaxFailedAccessAttempts = 4;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // for testing
                    //options.SignIn.RequireConfirmedAccount = true;
                })
                .AddRoles<ApplicationRole>()
                .AddUserManager<UserManager<ApplicationUser>>()
                .AddRoleManager<RoleManager<ApplicationRole>>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddEntityFrameworkStores<ContactsMgmtIdentityContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
        {
            //services.AddTransient<IContactRepository, TestContactRepository>(); // for testing
            //services.AddTransient<IContactRepository, CsvContactRepository>(); // for testing
            //services.AddTransient<IContactRepository, CsvHelperContactRepository>(); // for testing
            services.AddTransient<IContactRepository, SqlServerContactRepository>();
            services.AddTransient<IContactModificationRepository, SqlServerContactRepository>();
            services.AddTransient<IAuthenticationRepository, SqlServerAuthenticationRepository>();
            services.AddTransient<IUserRepository, SqlServerUserRepository>();
            services.AddTransient<IUserModificationRepository, SqlServerUserRepository>();
            services.AddTransient<IMeRepository, SqlServerMeRepository>();
            services.AddTransient<IJwtFactory, JwtFactory>();
            services.AddTransient<IRefreshTokenFactory, RefreshTokenFactory>();

            return services;
        }

        public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireStandard", policy => policy.RequireRole("Standard", "Manager", "HR", "Admin"));
                options.AddPolicy("RequireManager", policy => policy.RequireRole("Manager", "Admin"));
                options.AddPolicy("RequireHR", policy => policy.RequireRole("HR", "Admin"));
                options.AddPolicy("RequireAdmin", policy => policy.RequireRole("Admin"));
            });

            return services;
        }

        public static IServiceCollection ConfigureSession(this IServiceCollection services)
        {
            services.AddSession(options =>
            {
                //options.IdleTimeout = TimeSpan.FromMinutes(1);
            });

            return services;
        }

        public static IServiceCollection ConfigureSwag(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TodoApp", Version = "v1" });
                c.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme.ToLowerInvariant(),
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                //c.OperationFilter<AuthResponsesOperationFilter>();
            });

            return services;
        }

        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowedCorsOrigins",
                    builder =>
                    {
                        builder
                            //.SetIsOriginAllowed(IsOriginAllowed)
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            return services;
        }

        #region Private Methods

        private static bool IsOriginAllowed(string origin)
        {
            //var uri = new Uri(origin);
            //var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "n/a";

            //var isAllowed = uri.Host.Equals("example.com", StringComparison.OrdinalIgnoreCase)
            //                || uri.Host.Equals("another-example.com", StringComparison.OrdinalIgnoreCase)
            //                || uri.Host.EndsWith(".example.com", StringComparison.OrdinalIgnoreCase)
            //                || uri.Host.EndsWith(".another-example.com", StringComparison.OrdinalIgnoreCase);

            //if (!isAllowed && env.Contains("DEV", StringComparison.OrdinalIgnoreCase))
            //    isAllowed = uri.Host.StartsWith("localhost", StringComparison.OrdinalIgnoreCase);

            //return isAllowed;
            return true;
        }

        #endregion Private Methods
    }
}
