using CQRSBus.Locator;

namespace CQRSBus.Builder;

public static class HandlerLocatorBuilderExtension
{
    public static CommandBusBuilder AddDefaultHandlerLocator(this CommandBusBuilder builder)
    {
        var locatorNameInflector = new AdditionInflectionStrategy("Handler");
        var handlerLocator = new NameInflectorLocator(locatorNameInflector);

        return builder.SetHandlerLocator(handlerLocator);
    }
}
