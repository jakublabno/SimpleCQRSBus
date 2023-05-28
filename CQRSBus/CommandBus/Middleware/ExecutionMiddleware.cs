using CQRSBus.CommandBus.Specialized;
using CQRSBus.Locator;
using CQRSBus.Specialized;

namespace CQRSBus.CommandBus.Middleware;

public class ExecutionMiddleware : ICommandBusMiddleware
{
    private readonly IHandlerLocator handlerLocator;
    private readonly IHandlerCreator handlerCreator;

    public ExecutionMiddleware(IHandlerLocator handlerLocator, IHandlerCreator handlerCreator)
    {
        this.handlerLocator = handlerLocator;
        this.handlerCreator = handlerCreator;
    }

    public dynamic? Execute(ICommand message, Func<IMessage, dynamic>? callable)
    {
        var handlerName = handlerLocator.Name(message);
        var handler = handlerCreator.Create(handlerName);

        return handler
            .GetType()
            .GetMethod("Handle", types: new[] { message.GetType() })
            .Invoke(handler, new object[] { message });
    }
}
