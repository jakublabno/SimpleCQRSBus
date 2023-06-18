using CQRSBus.Locator;
using CQRSBus.Middleware;
using CQRSBus.QueryBus.Specialized;

namespace CQRSBus.QueryBus.Middleware;

public class QueryBusExecutionMiddleware : ExecutionMiddleware<IQuery>, IQueryBusMiddleware
{
    public QueryBusExecutionMiddleware(IHandlerLocator handlerLocator, IHandlerFactory handlerFactory) : base(handlerLocator, handlerFactory)
    {
    }
}
