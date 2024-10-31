using Microsoft.Extensions.Logging;

namespace SharedKernel.Logging;

public class LoggerManagerSerilog(ILogger<LoggerManagerSerilog> logger) : ILoggerManager
{
    public void LogDebug(string message)
    {
        logger.LogDebug(message);
    }

    public void LogError(Exception ex)
    {
        logger.LogError(ex, ex.Message);
    }

    public void LogInfo(string message)
    {
        logger.LogInformation(message);
    }

    public void LogWarn(string message)
    {
        logger.LogWarning(message);
    }
}
