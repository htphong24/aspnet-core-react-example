﻿using System.Text;
using AspnetCoreSPATemplate.Services.Authentication;
using AspnetCoreSPATemplate.Services.Common;
using AspnetCoreSPATemplate.Services.Common.Configuration;
using AspnetCoreSPATemplate.Services.Contacts;
using AspnetCoreSPATemplate.Services.Me;
using AspnetCoreSPATemplate.Services.Users;
using AutoMapper;
using Common.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SqlServerDataAccess.EF;
// ReSharper disable CheckNamespace

namespace AspnetCoreSPATemplate
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var jwtConfig = config.GetSection("JwtConfiguration").Get<JwtConfiguration>();
            services.Configure<JwtConfiguration>(config.GetSection("JwtConfiguration"));
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig.Issuer,
                    ValidAudience = jwtConfig.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key))
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
            // Auto Mapper
            services.AddAutoMapper(typeof(Startup));

            return services;
        }

        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
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

            return services;
        }

        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
        {
            //services.AddTransient<IContactRepository, TestContactRepository>();
            //services.AddTransient<IContactRepository, CsvContactRepository>();
            //services.AddTransient<IContactRepository, CsvHelperContactRepository>();
            services.AddTransient<IContactRepository, SqlServerContactRepository>();
            services.AddTransient<IContactModificationRepository, SqlServerContactRepository>();
            services.AddTransient<IAuthenticationRepository, SqlServerAuthenticationRepository>();
            services.AddTransient<IUserRepository, SqlServerUserRepository>();
            services.AddTransient<IUserModificationRepository, SqlServerUserRepository>();
            services.AddTransient<IMeRepository, SqlServerMeRepository>();

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
    }
}
