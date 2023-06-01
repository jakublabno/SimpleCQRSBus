using CQRSBus.CommandBus.Middleware;
using CQRSBus.Locator;

namespace CQRSBus.Builder;

public sealed class CommandBusBuilder
{
    private readonly List<Func<dynamic>> middlewareBuilders = new();
    
    private IHandlerLocator? handlerLocator;
    private IHandlerFactory? handlerFactory;
    

    public CommandBusBuilder AddMiddleware(ICommandBusMiddleware middleware)
    {
        middlewareBuilders.Add(() => middleware);

        return this;
    }

    public CommandBusBuilder SetHandlerLocator(IHandlerLocator handlerLocator)
    {
        this.handlerLocator = handlerLocator;

        return this;
    }

    public CommandBusBuilder SetHandlerFactory(IHandlerFactory handlerFactory)
    {
        this.handlerFactory = handlerFactory;

        return this;
    }
    
    public CommandBusBuilder AddExecutionMiddleware()
    {
        var middleware = () => new ExecutionMiddleware(handlerLocator, handlerFactory);
        
        middlewareBuilders.Add(middleware);

        return this;
    }

    public CommandBus.CommandBus Build()
    {
        var commandBusMiddlewares = new CommandBusMiddlewareCollection();
        
        foreach (var middleware in middlewareBuilders.Select(middlewareBuilder => middlewareBuilder.Invoke()))
        {
            commandBusMiddlewares.AddLast(middleware);
        }

        return new CommandBus.CommandBus(commandBusMiddlewares);
    }
}
