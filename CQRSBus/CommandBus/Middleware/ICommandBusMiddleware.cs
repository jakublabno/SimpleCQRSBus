using CQRSBus.CommandBus.Specialized;
using CQRSBus.Middleware;

namespace CQRSBus.CommandBus.Middleware;

public interface ICommandBusMiddleware : IMiddleware<ICommand>
{
}