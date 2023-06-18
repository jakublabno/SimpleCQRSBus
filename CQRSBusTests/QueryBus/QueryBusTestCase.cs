using CQRSBus.QueryBus.Middleware;
using CQRSBusTests.Helper;
using CQRSBusTests.QueryBus.Fixture;
using Microsoft.Extensions.Logging;

namespace CQRSBusTests.QueryBus;

public abstract class QueryBusTestCase
{
    private readonly InMemoryHandlerManager handlerManager = new();
    private readonly QueryBusMiddlewareCollection middlewareCollection = new();

    [SetUp]
    protected void Before()
    {
        middlewareCollection.Clear();
        handlerManager.Clear();
        handlerManager.Map(typeof(SampleQuery), new SampleQueryFinder());
    }

    protected CQRSBus.QueryBus.QueryBus QueryBus()
    {
        var bus = new CQRSBus.QueryBus.QueryBus(middlewareCollection);

        return bus;
    }

    protected void AddExecutorMiddleware()
    {
        AddMiddleWare(new QueryBusExecutionMiddleware(handlerManager, handlerManager));
    }

    protected void AddMiddleWare(params IQueryBusMiddleware[] middlewares)
    {
        foreach (var middleware in middlewares) middlewareCollection.Add(middleware);
    }

    private static class Logging
    {
        public static ILogger Get()
        {
            var factory = LoggerFactory.Create(builder => builder.AddConsole());

            return factory.CreateLogger("querybus tests");
        }
    }
}
