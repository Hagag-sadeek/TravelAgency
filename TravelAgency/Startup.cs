using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TravelAgency.Models;

namespace TravelAgency
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
            services.AddControllersWithViews();

            services.AddDbContext<TravelAgencyContext>(
             option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = "UserId";
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.IdleTimeout = TimeSpan.FromMinutes(400);

            });

            services.AddDistributedMemoryCache();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
              
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
             
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseStaticFiles(); 
            app.UseRouting(); 
            app.UseAuthorization(); 
            app.UseCookiePolicy();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
