using CQRSBus.Builder;
using CQRSBus.CommandBus.Middleware;
using CQRSBus.CommandBus.Specialized;
using CQRSBus.Locator;
using CQRSBus.Middleware;

namespace CQRSBus.CommandBus.Builder;

public sealed class CommandBusBuilder : BusBuilder<CommandBus, ICommandBusMiddleware, ICommand>
{
    protected override MiddlewareCollection<ICommandBusMiddleware, ICommand> CreateMiddlewareCollection()
    {
        return new CommandBusMiddlewareCollection();
    }

    protected override ExecutionMiddleware<ICommand> CreateExecutionMiddleware(IHandlerLocator handlerLocator, IHandlerFactory handlerFactory)
    {
        return new CommandBusExecutionMiddleware(handlerLocator, handlerFactory);
    }
}
