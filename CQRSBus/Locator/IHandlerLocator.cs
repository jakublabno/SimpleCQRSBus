using CQRSBus.Specialized;

namespace CQRSBus.Locator;

public interface IHandlerLocator
{
    string Name(IMessage message);
}