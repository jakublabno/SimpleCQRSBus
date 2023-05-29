using CQRSBus.CommandBus.Middleware;
using CQRSBus.CommandBus.Specialized;

namespace CQRSBus.CommandBus;

public class CommandBus : IBus<ICommand>
{
    private readonly BusMiddlewareAwareExecutor<ICommand, ICommandBusMiddleware, CommandBusMiddlewareCollection>
        executor;

    public CommandBus(CommandBusMiddlewareCollection commandBusMiddlewareCollection)
    {
        executor =
            new BusMiddlewareAwareExecutor<ICommand, ICommandBusMiddleware, CommandBusMiddlewareCollection>(
                commandBusMiddlewareCollection);
    }

    public TResult Handle<TResult>(ICommand message)
    {
        return (TResult)executor.Execute(message);
    }

    public void Handle(ICommand message)
    {
        executor.Execute(message);
    }
}
