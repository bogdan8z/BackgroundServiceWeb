using BackgroundServiceWeb.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BackgroundServiceWeb.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration _configuration;

        public ConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int WorkerDelaySeconds()
        {
            if (!int.TryParse(_configuration["BackgroundService1:WorkerDelaySeconds"], out var seconds))
            {
                seconds = 10;
            }

            return seconds;
        }
    }
}
