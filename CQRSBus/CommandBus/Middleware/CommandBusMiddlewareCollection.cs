using CQRSBus.CommandBus.Specialized;
using CQRSBus.Middleware;

namespace CQRSBus.CommandBus.Middleware;

public class CommandBusMiddlewareCollection : MiddlewareCollection<ICommandBusMiddleware, ICommand>
{
}