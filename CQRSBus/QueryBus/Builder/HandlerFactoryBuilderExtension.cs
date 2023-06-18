using CQRSBus.Locator;

namespace CQRSBus.QueryBus.Builder;

public static class HandlerFactoryBuilderExtension
{
    public static QueryBusBuilder AddDefaultHandlerFactory(this QueryBusBuilder builder)
    {
        return (QueryBusBuilder)builder.SetHandlerFactory(new SimpleHandlerActivator());
    }
}
