using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
                        "data source=server.microex.cn;initial catalog=aixinyaoye.bonusshow;user id=sa;password=lulus@aliyun123;MultipleActiveResultSets=True;App=EntityFramework");
                });
            services.AddSingleton<WXConfig>();
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
                
            });
        }
    }
}
