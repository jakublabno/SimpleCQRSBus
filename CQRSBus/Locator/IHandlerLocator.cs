using CQRSBus.Specialized;

namespace CQRSBus.Locator;

public interface IHandlerLocator<in T> where T : IMessage
{
    Type Name(IMessage message);
}
