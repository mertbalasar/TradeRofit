using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeRofit.API.Mapper;
using TradeRofit.API.Middlewares;
using TradeRofit.Business.Interfaces;
using TradeRofit.Business.Models.Configures;
using TradeRofit.Business.Services;
using TradeRofit.DAL.Models.Configures;
using TradeRofit.DAL.Repository;

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
            services.Configure<MongoSettings>(Configuration.GetSection("MongoSettings"));
            services.AddSingleton(serviceProvider => serviceProvider.GetRequiredService<IOptions<MongoSettings>>().Value);
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddSingleton(serviceProvider => serviceProvider.GetRequiredService<IOptions<AppSettings>>().Value);

            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            services.AddScoped(typeof(IHomeService), typeof(HomeService));
            services.AddScoped(typeof(ITradingViewRestService), typeof(TradingViewRestService));
            services.AddScoped(typeof(IUserService), typeof(UserService));

            services.AddAutoMapper(typeof(MappingProfile));
            services.AddHttpContextAccessor();
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "TradeRofit API",
                    Description = "A Swagger Documentation For TradeRofit Project",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Mert Balasar",
                        Email = "mertblsr@gmail.com"
                    }
                });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                c.DocumentTitle = "TradeRofit API";
            });

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
