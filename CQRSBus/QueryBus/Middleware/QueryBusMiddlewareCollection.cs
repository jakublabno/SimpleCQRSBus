using CQRSBus.Middleware;
using CQRSBus.QueryBus.Specialized;

namespace CQRSBus.QueryBus.Middleware;

public class QueryBusMiddlewareCollection : MiddlewareCollection<IQueryBusMiddleware, IQuery>
{
}
