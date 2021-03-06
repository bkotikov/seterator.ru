﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Rauthor.Services;


namespace Rauthor
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        string Connection => Configuration.GetConnectionString("Local MySQL");

        public Startup(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            Contract.Assert(env != null);
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json",
                             optional: false,
                             reloadOnChange: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }
            Configuration = builder.Build();
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services
                .Configure<CookiePolicyOptions>(options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                })
                .AddDistributedMemoryCache()
                .AddSession()
                .AddLogging()
                .AddPrimitiveMemoryCache()
                .AddFoulLanguageFilter("*")
                .AddDbContext<DatabaseContext>(options => options.UseMySQL(Connection)
                                                                         .EnableDetailedErrors()
                                                                         .EnableSensitiveDataLogging())
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = new PathString("/Account/Main");
                        options.Cookie.Name = "ssid";
                    })
                    ;
            services
                .AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                    .AddMvcOptions(options => options.EnableEndpointRouting = false);

        }
#pragma warning disable CA1822 // Member Configure does not access instance data and can be marked as static
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
#pragma warning restore CA1922 
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection()
               .UseStaticFiles()
               .UseCookiePolicy()
               .UseSession()
               .UseAuthentication()
               .UseMiddleware<Services.SessionRestore>()
               .UseMvc(routes =>
               {
                   routes.MapRoute(
                       name: "Guid parameter",
                       template: "{controller}/{action}/{guid}",
                       defaults: new { controller="Home", action="Index" },
                       constraints: new { guid = new GuidRouteConstraint() });
                   routes.MapRoute(
                       name: "Only action",
                       template: "{controller=Home}/{action=Index}");
               });
            
        }
    }
}
