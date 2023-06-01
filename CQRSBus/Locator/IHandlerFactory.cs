namespace CQRSBus.Locator;

public interface IHandlerFactory
{
    public object Create(string handlerName);
}
