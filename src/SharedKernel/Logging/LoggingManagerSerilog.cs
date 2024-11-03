using Microsoft.Extensions.Logging;

namespace SharedKernel.Logging;

public class LoggerManagerSerilog(ILogger<LoggerManagerSerilog> logger) : ILoggerManager
{
    public void LogDebug(string message)
    {
        logger.LogDebug("{Message}", message);
    }

    public void LogError(Exception ex)
    {
        logger.LogError(ex, "{ExceptionMessage}", ex.Message);
    }

    public void LogInfo(string message)
    {
        logger.LogInformation("{Message}", message);
    }

    public void LogWarn(string message)
    {
        logger.LogWarning("{Message}", message);
    }
}
