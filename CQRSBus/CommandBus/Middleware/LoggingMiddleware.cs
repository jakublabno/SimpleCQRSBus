using CQRSBus.CommandBus.Specialized;
using CQRSBus.Specialized;
using Microsoft.Extensions.Logging;

namespace CQRSBus.CommandBus.Middleware;

public class LoggingMiddleware : ICommandBusMiddleware
{
    private readonly ILogger logger;

    public LoggingMiddleware(ILogger logger)
    {
        this.logger = logger;
    }

    public dynamic? Execute(ICommand message, Func<IMessage, dynamic>? callable)
    {
        logger.Log(LogLevel.Information, $"Starting command {message.GetType().FullName}");

        var del = (IMessage message) => callable(message);

        return del;
    }
}
