using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using KancelarCloud.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace KancelarCloud
{
    public class Startup
    {
        //public Startup(IHostingEnvironment env)
        //{
        //    //var builder = new ConfigurationBuilder()
        //    //    .SetBasePath(env.ContentRootPath)
        //    //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        //    //    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
        //    //    .AddEnvironmentVariables();
        //    //Configuration = builder.Build();



        //}
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string Connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<LoginContext>(options => options.UseSqlServer(Configuration.GetConnectionString("def")));
            services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 5;//минимальная длинна пароля
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<LoginContext>();
            services.AddDbContext<ContextDBcs>(options => options.UseSqlServer(Connection));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
            options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Logins");
            });
            services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/Logins");
            // Add framework services.
            services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = int.MaxValue;
                options.MultipartBodyLengthLimit = long.MaxValue;
                options.MultipartHeadersLengthLimit = int.MaxValue;
            });
           
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

         
        }
    }
}
