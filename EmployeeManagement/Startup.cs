using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement
{
    public class Startup
    {


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>
            (options => options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));


            //if Use IdentityDBContext and also Configure Pasword Policys Then Following Code to works
            //services.AddIdentity<IdentityUser, IdentityRole>(option=> {
            //    option.Password.RequiredLength = 10;
            //    option.Password.RequiredUniqueChars = 10;
            //    option.Password.RequireNonAlphanumeric = false;

            //}).AddEntityFrameworkStores<AppDbContext>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            //For Configure Identity Password Policy
            services.Configure<IdentityOptions>(option =>
            {
                option.Password.RequiredLength = 10;
                option.Password.RequiredUniqueChars = 3;
                option.Password.RequireNonAlphanumeric = false;
            });
                services.AddMvc(option=>
                {
                    //For Global Authontication Setting for Entile Application
                    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                    .Build();
                    option.Filters.Add(new AuthorizeFilter(policy));


                }).AddXmlSerializerFormatters();
         
            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
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
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
            }

            app.UseStaticFiles();
            //app.UseMvcWithDefaultRoute();
            app.UseAuthentication();
            app.UseMvc(routes=>
            {
                routes.MapRoute("default", "{controller=Home}/{action=index}/{id?}");
            });

            ////app.Run(async (context) =>
            ////{
            ////    /*await context.Response.WriteAsync("Hello World!");*/
            ////    await context.Response.WriteAsync("Hosting Environment: " + env.EnvironmentName);
            ////});
        }
    }
}
