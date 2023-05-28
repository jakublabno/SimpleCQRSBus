namespace CQRSBus.Locator;

public interface IHandlerCreator
{
    public object Create(Type handlerName);
}
