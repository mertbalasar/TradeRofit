using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TradeRofit.DAL.Repository;
using TradeRofit.Entities.Models;

namespace TradeRofit.TokenWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMongoRepository<User> _userRepository;

        public Worker(ILogger<Worker> logger,
            IMongoRepository<User> userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;

            _logger.LogInformation("Worker starting");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                
                await Task.Delay(100, stoppingToken);
            }
        }
    }
}
