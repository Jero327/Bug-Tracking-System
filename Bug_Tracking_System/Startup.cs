using Bug_Tracking_System.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Bug_Tracking_System
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Environment = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            
            // services.AddDbContext<BugContext>(options =>
            // {
            //     var connectionString = Configuration.GetConnectionString("BugContext");

            //     if (Environment.IsDevelopment())
            //     {
            //         options.UseSqlite(connectionString);
            //     }
            //     else
            //     {
            //         options.UseSqlServer(connectionString);
            //     }
            // });

            // services.AddDbContext<UserContext>(options =>
            // {
            //     var connectionString = Configuration.GetConnectionString("UserContext");

            //     if (Environment.IsDevelopment())
            //     {
            //         options.UseSqlite(connectionString);
            //     }
            //     else
            //     {
            //         options.UseSqlServer(connectionString);
            //     }
            // });

            // services.AddDbContext<EnrollmentContext>(options =>
            // {
            //     var connectionString = Configuration.GetConnectionString("EnrollmentContext");

            //     if (Environment.IsDevelopment())
            //     {
            //         options.UseSqlite(connectionString);
            //     }
            //     else
            //     {
            //         options.UseSqlServer(connectionString);
            //     }
            // });

            // services.AddDbContext<ProjectContext>(options =>
            // {
            //     var connectionString = Configuration.GetConnectionString("ProjectContext");

            //     if (Environment.IsDevelopment())
            //     {
            //         options.UseSqlite(connectionString);
            //     }
            //     else
            //     {
            //         options.UseSqlServer(connectionString);
            //     }
            // });

            // services.AddDbContext<SubProjectContext>(options =>
            // {
            //     var connectionString = Configuration.GetConnectionString("SubProjectContext");

            //     if (Environment.IsDevelopment())
            //     {
            //         options.UseSqlite(connectionString);
            //     }
            //     else
            //     {
            //         options.UseSqlServer(connectionString);
            //     }
            // });

            // services.AddDbContext<TestCaseContext>(options =>
            // {
            //     var connectionString = Configuration.GetConnectionString("TestCaseContext");

            //     if (Environment.IsDevelopment())
            //     {
            //         options.UseSqlite(connectionString);
            //     }
            //     else
            //     {
            //         options.UseSqlServer(connectionString);
            //     }
            // });

            services.AddDbContext<BugContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("MyDbConnection")));
            services.AddDbContext<UserContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("MyDbConnection")));
            services.AddDbContext<EnrollmentContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("MyDbConnection")));
            services.AddDbContext<ProjectContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("MyDbConnection")));
            services.AddDbContext<SubProjectContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("MyDbConnection")));
            services.AddDbContext<TestCaseContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("MyDbConnection")));
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
