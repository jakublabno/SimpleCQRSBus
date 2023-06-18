using CQRSBus.Middleware;
using CQRSBus.QueryBus.Specialized;

namespace CQRSBus.QueryBus.Middleware;

public interface IQueryBusMiddleware : IMiddleware<IQuery>
{
}
