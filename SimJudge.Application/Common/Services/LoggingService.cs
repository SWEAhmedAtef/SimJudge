using Serilog;
using SimJudge.Application.Common.Interfaces;

namespace SimJudge.Application.Common.Services
{
    public class LoggingService : ILoggingService
    {
        private readonly ILogger _logger;

        public LoggingService()
        {
            _logger = Log.ForContext<LoggingService>();
        }

        public void LogInformation(string message, params object[] args)
        {
            _logger.Information(message, args);
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.Warning(message, args);
        }

        public void LogError(string message, Exception? exception = null, params object[] args)
        {
            if (exception != null)
            {
                _logger.Error(exception, message, args);
            }
            else
            {
                _logger.Error(message, args);
            }
        }

        public void LogDebug(string message, params object[] args)
        {
            _logger.Debug(message, args);
        }

        public void LogBusinessEvent(string eventName, object? data = null, string? userId = null)
        {
            _logger.Information(
                "Business Event: {EventName} | User: {UserId} | Data: {@Data} | Timestamp: {Timestamp}",
                eventName, userId, data, DateTime.UtcNow);
        }
    }
}
