using CQRSBus.CommandBus.Middleware;
using CQRSBus.Locator;

namespace CQRSBus.Builder;

public static class HandlerFactoryBuilderExtension
{
    private delegate ExecutionMiddleware BuildExecutionMiddleware(IHandlerLocator handlerLocator, IHandlerFactory handlerFactory);
    
    public static CommandBusBuilder AddDefaultHandlerFactory(this CommandBusBuilder builder)
    {
        return builder.SetHandlerFactory(new SimpleHandlerActivator());
    }
}
