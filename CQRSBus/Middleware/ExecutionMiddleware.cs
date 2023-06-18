using CQRSBus.Locator;
using CQRSBus.Specialized;

namespace CQRSBus.Middleware;

public abstract class ExecutionMiddleware<T> : IMiddleware<T> where T : IMessage
{
    private readonly IHandlerFactory handlerFactory;
    private readonly IHandlerLocator handlerLocator;

    protected ExecutionMiddleware(IHandlerLocator handlerLocator, IHandlerFactory handlerFactory)
    {
        this.handlerLocator = handlerLocator;
        this.handlerFactory = handlerFactory;
    }

    public dynamic? Execute(T message, Func<IMessage, dynamic>? callable)
    {
        var handlerName = handlerLocator.Name(message);
        var handler = handlerFactory.Create(handlerName);

        return handler
            .GetType()
            .GetMethod("Handle", new[] { message.GetType() })!
            .Invoke(handler, new object[] { message });
    }
}
