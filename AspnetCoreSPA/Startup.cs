using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BotDetect.Web;

namespace AspnetCoreSPATemplate
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private static IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services
                .ConfigureAuthentication(Configuration)
                .ConfigureDatabase(Configuration)
                .ConfigureDependencyInjection()
                .ConfigureIdentity()
                .ConfigureApplicationCookie(options => options.LoginPath = "/auth/login")
                .ConfigureAuthorization()
                .ConfigureOtherServices();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            // In production, the SPA files will be served from this directory
            services.AddSpaStaticFiles(configuration => configuration.RootPath = "app/dist");
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

            // configure BotDetectCaptcha
            app.UseSimpleCaptcha(Configuration.GetSection("BotDetect"));

            app.UseAuthentication();

            app.UseMvc(routes => routes.MapRoute(
                name: "default",
                template: "{controller}/{action=Index}/{id?}"));

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "app";

                if (env.IsDevelopment())
                    //spa.UseProxyToSpaDevelopmentServer("http://localhost:8080");
                    spa.UseReactDevelopmentServer(npmScript: "start");
            });
        }
    }
}