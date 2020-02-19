using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoShop_Shared.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoShop_Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AutoShop_Shared.Controllers;

namespace AutoShop_Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration);

            services.AddSingleton(typeof(IUserService), typeof(UserService));

            services.AddSingleton(typeof(IBadgeService), typeof(BadgeService));

            services.AddSingleton(typeof(IRepository<>), typeof(CosmosDBRepository<>));
            
            services.Configure<AzureStorageConfig>(Configuration.GetSection("AzureStorageConfig"));
            
            //connexion vers la BD et mapping avec AuthenticationContext
            services.AddDbContext<AuthenticationContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("AuthenticationContextConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => 
            options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<AuthenticationContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();


            services.AddControllersWithViews();
            services.AddRazorPages();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
