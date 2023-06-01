using CQRSBus.CommandBus.Specialized;
using CQRSBus.Locator;
using CQRSBus.Specialized;

namespace CQRSBus.CommandBus.Middleware;

public class ExecutionMiddleware : ICommandBusMiddleware
{
    private readonly IHandlerFactory handlerFactory;
    private readonly IHandlerLocator handlerLocator;

    public ExecutionMiddleware(IHandlerLocator handlerLocator, IHandlerFactory handlerFactory)
    {
        this.handlerLocator = handlerLocator;
        this.handlerFactory = handlerFactory;
    }

    public dynamic? Execute(ICommand message, Func<IMessage, dynamic>? callable)
    {
        var handlerName = handlerLocator.Name(message);
        var handler = handlerFactory.Create(handlerName);

        return handler
            .GetType()
            .GetMethod("Handle", new[] { message.GetType() })!
            .Invoke(handler, new object[] { message });
    }
}
