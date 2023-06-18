using CQRSBus.QueryBus.Middleware;
using CQRSBus.QueryBus.Specialized;

namespace CQRSBus.QueryBus;

public class QueryBus : IBus<IQuery>
{
    private readonly BusMiddlewareAwareExecutor<IQuery, IQueryBusMiddleware, QueryBusMiddlewareCollection>
        executor;

    public QueryBus(QueryBusMiddlewareCollection queryBusMiddlewareCollection)
    {
        executor =
            new BusMiddlewareAwareExecutor<IQuery, IQueryBusMiddleware, QueryBusMiddlewareCollection>(
                queryBusMiddlewareCollection);
    }

    public TResult Handle<TResult>(IQuery message)
    {
        return (TResult)executor.Execute(message);
    }
}
