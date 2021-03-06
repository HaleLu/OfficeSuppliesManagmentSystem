﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OfficeSuppliesManagementSystem.Models;

namespace OfficeSuppliesManagementSystem
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();

                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            services.AddIdentity<ApplicationUser, ApplicationRole>(
                options =>
                {
                    options.Password = new PasswordOptions()
                    {
                        RequireDigit = false,
                        RequireLowercase = false,
                        RequireUppercase = false,
                        RequireNonLetterOrDigit = false,
                        RequiredLength = 1
                    };
                })
                .AddEntityFrameworkStores<ApplicationDbContext, int>()
                .AddDefaultTokenProviders();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                       .CreateScope())
            {
                serviceScope.ServiceProvider.GetService<ApplicationDbContext>()
                     .Database.EnsureCreated();
            }

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
                try
                {
                    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                        .CreateScope())
                    {
                        serviceScope.ServiceProvider.GetService<ApplicationDbContext>()
                             .Database.Migrate();
                    }
                }
                catch { }
            }

            app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseIdentity();

            // To configure external authentication please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Supply}/{action=Index}/{id?}");
            });

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ViewModels.ApplicationUser.EditViewModel, ApplicationUser>();
                cfg.CreateMap<ApplicationUser, ViewModels.ApplicationUser.EditViewModel>();
                cfg.CreateMap<ViewModels.ImportRecord.CreateViewModel, ImportRecord>();
                cfg.CreateMap<ImportRecord, ViewModels.ImportRecord.CreateViewModel>();
                cfg.CreateMap<ViewModels.ExportRecord.CreateViewModel, ExportRecord>();
                cfg.CreateMap<ExportRecord, ViewModels.ExportRecord.CreateViewModel>();
                cfg.CreateMap<ViewModels.Supply.EditViewModel, Supply>();
                cfg.CreateMap<Supply, ViewModels.Supply.EditViewModel>();
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
