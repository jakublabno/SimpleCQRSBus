using CQRSBus.Locator;
using CQRSBus.Middleware;
using CQRSBus.Specialized;

namespace CQRSBus.Builder;

public abstract class BusBuilder<T, TX, TRx> where TX : IMiddleware<TRx> where TRx : IMessage
{
    private readonly List<Func<dynamic>> middlewareBuilders = new();
    
    private IHandlerLocator? handlerLocator;
    private IHandlerFactory? handlerFactory;

    protected abstract MiddlewareCollection<TX, TRx> CreateMiddlewareCollection();
    protected abstract ExecutionMiddleware<TRx> CreateExecutionMiddleware(IHandlerLocator handlerLocator, IHandlerFactory handlerFactory);

    public BusBuilder<T, TX, TRx> AddMiddleware(TX middleware)
    {
        middlewareBuilders.Add(() => middleware);

        return this;
    }

    public BusBuilder<T, TX, TRx> SetHandlerLocator(IHandlerLocator handlerLocator)
    {
        this.handlerLocator = handlerLocator;

        return this;
    }

    public BusBuilder<T, TX, TRx> SetHandlerFactory(IHandlerFactory handlerFactory)
    {
        this.handlerFactory = handlerFactory;

        return this;
    }
    
    public BusBuilder<T, TX, TRx> AddExecutionMiddleware()
    {
        var middleware = () => CreateExecutionMiddleware(handlerLocator, handlerFactory);
        
        middlewareBuilders.Add(middleware);

        return this;
    }

    public T Build()
    {
        var busMiddlewares = CreateMiddlewareCollection();
        
        foreach (var middleware in middlewareBuilders.Select(middlewareBuilder => middlewareBuilder.Invoke()))
        {
            busMiddlewares.Add(middleware);
        }

        return ((T)Activator.CreateInstance(typeof(T), busMiddlewares))!;
    }
}
