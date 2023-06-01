namespace CQRSBus.Locator;

public class SimpleHandlerActivator : IHandlerFactory
{
    public object Create(string handlerName)
    {
        var type = Type.GetType(handlerName, true);
        
        return Activator.CreateInstance(type);
    }
}
