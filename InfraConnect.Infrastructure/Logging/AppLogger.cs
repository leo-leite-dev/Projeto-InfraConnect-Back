using InfraConnect.Application.Commons;
using Microsoft.Extensions.Logging;

namespace InfraConnect.Infrastructure.Logging
{
    public class AppLogger<T> : IAppLogger<T>
    {
        private readonly ILogger<T> _logger;

        public AppLogger(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void LogInformation(string message) => _logger.LogInformation(message);
        public void LogWarning(Exception ex, string message) => _logger.LogWarning(ex, message);
        public void LogError(Exception ex, string message) => _logger.LogError(ex, message);
    }
}