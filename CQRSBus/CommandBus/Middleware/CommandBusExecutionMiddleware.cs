using CQRSBus.CommandBus.Specialized;
using CQRSBus.Locator;
using CQRSBus.Middleware;

namespace CQRSBus.CommandBus.Middleware;

public class CommandBusExecutionMiddleware : ExecutionMiddleware<ICommand>, ICommandBusMiddleware
{
    public CommandBusExecutionMiddleware(IHandlerLocator handlerLocator, IHandlerFactory handlerFactory) : base(handlerLocator, handlerFactory)
    {
    }
}
