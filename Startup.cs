using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExploreCalifornia1.Data;
using ExploreCalifornia1.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExploreCalifornia1
{
    public class Startup
    {
        public IConfiguration Config { get; }

        public Startup(IConfiguration config)
        {
            Config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<IdentityDataContext>();
            
            services.AddAuthentication()
                .AddCookie();

            services
               .AddControllersWithViews()
               .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);
            services.AddRazorPages().AddRazorRuntimeCompilation();

            services.AddDbContext<BlogDataContext>(options =>
                options.UseSqlServer(Config.GetConnectionString("BlogConnect"))
            );

            services.AddDbContext<IdentityDataContext>(options =>
                options.UseSqlServer(Config.GetConnectionString("BlogConnect"))
            );

            services.AddSingleton<FormattingService>();
            
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (Config.GetValue<bool>("UseDeveloperExceptionPage"))
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseNodeModules();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(cfg =>
            {
                cfg.MapDefaultControllerRoute();

                //The above method, MapDefaultControllerRoute is a helper method for the default route commented out directly below

                //cfg.MapControllerRoute("Fallback",
                //    "{controller}/{action}/{id?}",
                //    new { controller = "App", action = "Index" }
                //);

                cfg.MapControllers();   //required to examine Route attributes on controller actions
                cfg.MapRazorPages();
            });
        }
    }
}
