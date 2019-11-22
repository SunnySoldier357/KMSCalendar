﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using KMSCalendar.MobileAppService.Models.Entities;
using KMSCalendar.MobileAppService.Models.Environment;

namespace KMSCalendar.MobileAppService
{
    public class Startup
    {
        //* Static Properties
        public static bool isDevelopment = false;

        //* Private Properties
        private readonly Configuration configuration;

        //* Constructors
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            isDevelopment = env.IsDevelopment();

            this.configuration = new Configuration();
            configuration.Bind(this.configuration);
        }

        //* Public Methods

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure
        /// the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
            {
                app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute("Default",
                    "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<CalendarDbDataContext>(options =>
                options.UseSqlServer(configuration.ConnectionStrings.ConnectionString));
        }
    }
}