using System;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Hosting;
using NLog;
using BackgroundServiceWeb.Services.Interfaces;

namespace BackgroundServiceWeb.Tasks
{
    public class DoSomethingEveryXHoursBackgroundService : BackgroundService
    {
        private readonly NLog.ILogger _logger;
        private readonly IConfigurationService _configurationService;
        
        public DoSomethingEveryXHoursBackgroundService(IServiceProvider serviceProvider)
        {
            _logger = LogManager.GetCurrentClassLogger();
            _configurationService = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IConfigurationService>();
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    await DoWorkAsync();
                    await Task.Delay(WorkerDelayMiliseconds, stoppingToken);
                }
            }
            catch (TaskCanceledException)
            {
                // When the stopping token is canceled, for example, a call made from services.msc,
                // we shouldn't exit with a non-zero exit code. In other words, this is expected...
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error, give me more details");
            }
        }

        private int WorkerDelayMiliseconds
        {
            get
            {
                return 1000 * _configurationService.WorkerDelaySeconds();
            }
        }


        private Task DoWorkAsync()
        {
            //do something on every 10seconds
            //..
            Console.WriteLine($"background work: {DateTime.Now}");

            return Task.CompletedTask;
        }
    }
}
