using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TradeRofit.DAL.Models.Configures;
using TradeRofit.DAL.Repository;
using TradeRofit.Entities.Models;

namespace TradeRofit.TokenWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();

                    services.Configure<MongoSettings>(hostContext.Configuration.GetSection("MongoSettings"));
                    services.AddSingleton(serviceProvider => serviceProvider.GetRequiredService<IOptions<MongoSettings>>().Value);

                    services.AddSingleton(typeof(IMongoRepository<>), typeof(MongoRepository<>));
                }).UseSerilog();
    }
}
