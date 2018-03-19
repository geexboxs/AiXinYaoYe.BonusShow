using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AiXinYaoYeV2.Database;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;

namespace AiXinYaoYeV2
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
            services.AddAuthorization(options => { options.DefaultPolicy = new AuthorizationPolicy(new List<IAuthorizationRequirement>(){new ClaimsAuthorizationRequirement(ClaimTypes.NameIdentifier,new List<string>(){"admin"})},new List<string>(){ CookieAuthenticationDefaults.AuthenticationScheme } ); });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => { options.LoginPath = "/admin/login/login"; });
            services.AddLogging(options =>
            {
                options.AddEventLog();
                options.AddProvider(new EventLogLoggerProvider());
                options.SetMinimumLevel(LogLevel.Warning);
            });
            services.AddMvc();
            services.AddMemoryCache();
            services.AddDbContext<Database.MyDbContext>(options =>
                {
                    options.UseSqlServer(
                        Configuration.GetConnectionString("MyDbContext"));
                });
            services.AddSingleton<AiXinYaoYeDb>((_) => new AiXinYaoYeDb(Configuration.GetConnectionString("AiXinYaoYeDb"),new EventLogLogger("AiXinYaoYeDb")));
            services.AddSession();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSession();
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller}/{action}/{id?}"
                );
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}"
                );

            });
        }
    }
}
