namespace SimJudge.Application.Common.Interfaces
{
    public interface ILoggingService
    {
        void LogInformation(string message, params object[] args);
        void LogWarning(string message, params object[] args);
        void LogError(string message, Exception? exception = null, params object[] args);
        void LogDebug(string message, params object[] args);
        void LogBusinessEvent(string eventName, object? data = null, string? userId = null);
    }
}
