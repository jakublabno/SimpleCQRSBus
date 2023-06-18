using CQRSBus.Builder;
using CQRSBus.Locator;
using CQRSBus.Middleware;
using CQRSBus.QueryBus.Middleware;
using CQRSBus.QueryBus.Specialized;

namespace CQRSBus.QueryBus.Builder;

public sealed class QueryBusBuilder : BusBuilder<QueryBus, IQueryBusMiddleware, IQuery>
{
    protected override MiddlewareCollection<IQueryBusMiddleware, IQuery> CreateMiddlewareCollection()
    {
        return new QueryBusMiddlewareCollection();
    }

    protected override ExecutionMiddleware<IQuery> CreateExecutionMiddleware(IHandlerLocator handlerLocator, IHandlerFactory handlerFactory)
    {
        return new QueryBusExecutionMiddleware(handlerLocator, handlerFactory);
    }
}
