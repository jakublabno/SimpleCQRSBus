using CQRSBus.CommandBus.Middleware;
using CQRSBus.Locator;
using CQRSBusTests.CommandBus.Double;
using Microsoft.Extensions.Logging;

namespace CQRSBusTests.CommandBus;

public abstract class CommandBusTestCase
{
    private readonly NameInflectorLocator handlerLocator = new("Handler");
    private readonly CommandBusMiddlewareCollection middlewareCollection = new();

    [SetUp]
    protected void Before()
    {
        middlewareCollection.Clear();
    }

    protected CQRSBus.CommandBus.CommandBus CommandBus()
    {
        var bus = new CQRSBus.CommandBus.CommandBus(middlewareCollection);

        return bus;
    }
    
    protected void AddLoggerMiddleware()
    {
        AddMiddleWare(new LoggingMiddleware(Logging.Get()));
    }

    protected void AddExecutorMiddleware()
    {
        AddMiddleWare(new ExecutionMiddleware(handlerLocator, new SimpleHandlerActivator()));
    }
    
    protected void AddMiddleWare(params ICommandBusMiddleware[] middlewares)
    {
        foreach (var middleware in middlewares)
        {
            middlewareCollection.AddLast(middleware);
        }
    }

    private static class Logging
    {
        public static ILogger Get()
        {
            var factory = LoggerFactory.Create(builder => builder.AddConsole());

            return factory.CreateLogger("commandbus tests");
        }
    }
}
