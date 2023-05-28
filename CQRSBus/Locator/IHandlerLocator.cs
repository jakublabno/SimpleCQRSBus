using CQRSBus.Specialized;

namespace CQRSBus.Locator;

public interface IHandlerLocator
{
    Type Name(IMessage message);
}
