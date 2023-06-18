using CQRSBus.Locator;

namespace CQRSBus.CommandBus.Builder;

public static class HandlerFactoryBuilderExtension
{
    public static CommandBusBuilder AddDefaultHandlerFactory(this CommandBusBuilder builder)
    {
        return (CommandBusBuilder)builder.SetHandlerFactory(new SimpleHandlerActivator());
    }
}
