using Microsoft.Extensions.Logging;

namespace FilmLand.Logs
{
    public class CustomLogger : ICustomLogger
    {
        private readonly ILogger<CustomLogger> _logger;

        public CustomLogger(ILogger<CustomLogger> logger)
        {
            _logger = logger;
        }
        public void ErrorDatabase(string err)
        {
            _logger.LogError($"DATABASE: {err}");
        }
        public void SuccessDatabase(string message)
        {
            _logger.LogInformation($"DATABASE: {message}");
        }
        public void StartAPI(string apiName)
        {
            _logger.LogInformation($"API: Start '{apiName}' Api");
        }
        public void EndAPI(string apiName)
        {
            _logger.LogInformation($"API: End '{apiName}' Api");
        }
        public void CustomApiError(string message)
        {
            _logger.LogError($"API: {message}");
        }
        public void CustomDatabaseError(string message)
        {
            _logger.LogError($"DATABASE: {message}");
        }
    }
}
