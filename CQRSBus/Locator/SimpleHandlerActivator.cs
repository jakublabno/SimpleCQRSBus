namespace CQRSBus.Locator;

public class SimpleHandlerActivator : IHandlerCreator
{
    public object Create(Type handlerName)
    {
        return Activator.CreateInstance(handlerName);
    }
}
