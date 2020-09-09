using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BotDetect.Web;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace AspnetCoreSPATemplate
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddControllersWithViews();
            services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);
            //services.Configure<IISServerOptions>(options => options.AllowSynchronousIO = true);

            services
                .ConfigureAuthentication(Configuration)
                .ConfigureDatabase(Configuration)
                .ConfigureDependencyInjection()
                .ConfigureIdentity()
                .ConfigureApplicationCookie(options => options.LoginPath = "/auth/login")
                .ConfigureAuthorization()
                .ConfigureSession()
                .ConfigureOtherServices();

            // In production, the SPA files will be served from this directory
            services.AddSpaStaticFiles(configuration => configuration.RootPath = "app/dist");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseSpaStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            // for .netcoreapp3.1
            app.UseEndpoints(endpoints => endpoints.MapControllers());

            // for .netcoreapp2.2
            //app.UseMvc(routes => routes.MapRoute(
            //    name: "default",
            //    template: "{controller}/{action=Index}/{id?}"));
            app.UseSimpleCaptcha(Configuration.GetSection("BotDetect"));

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