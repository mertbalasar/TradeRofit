using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeRofit.API.Middlewares;
using TradeRofit.Business.Interfaces;
using TradeRofit.Business.Models.Configures;
using TradeRofit.Business.Services;

namespace TradeRofit
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
            services.Configure<VersionInfo>(Configuration.GetSection("VersionInfo"));
            services.AddSingleton(serviceProvider => serviceProvider.GetRequiredService<IOptions<VersionInfo>>().Value);
            services.Configure< TradingViewConfigures>(Configuration.GetSection("TradingViewConfigures"));
            services.AddSingleton(serviceProvider => serviceProvider.GetRequiredService<IOptions<TradingViewConfigures>>().Value);

            services.AddScoped(typeof(IHomeService), typeof(HomeService));
            services.AddScoped(typeof(ITradingViewRestService), typeof(TradingViewRestService));

            services.AddHttpContextAccessor();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware(typeof(UserMiddleware));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
