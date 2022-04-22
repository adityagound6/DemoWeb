using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DemoWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace DemoWeb
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration _config)
        {
            this._config = _config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(option => 
            option.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));
            services.AddMvc(_config => {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                _config.Filters.Add(new AuthorizeFilter(policy));
            });
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddScoped<IEmployee, SqlEmployeeRepositry>(); 
            services.AddIdentity<ApplicationUser, IdentityRole>(option=> { 
            
            }).AddEntityFrameworkStores<AppDbContext>();
            //MvcOption
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
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
            }

            app.UseRouting();

            //app.UseMvc();
            app.UseStaticFiles();

            app.UseAuthentication();

            //app.UseMvcWithDefaultRoute();
            app.UseMvc(route => {
               route.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            
            });

            //app.UseMvc();
        
            
        }
    }
}
