using CQRSBus.Locator;

namespace CQRSBus.QueryBus.Builder;

public static class HandlerLocatorBuilderExtension
{
    public static QueryBusBuilder AddDefaultHandlerLocator(this QueryBusBuilder builder)
    {
        var locatorNameInflector = new AdditionInflectionStrategy("Finder");
        var handlerLocator = new NameInflectorLocator(locatorNameInflector);

        return (QueryBusBuilder)builder.SetHandlerLocator(handlerLocator);
    }
}
