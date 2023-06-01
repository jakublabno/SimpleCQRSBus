using CQRSBus.Locator;
using CQRSBus.Specialized;

namespace CQRSBusTests.CommandBus.Double;

public class InMemoryHandlerManager : IHandlerLocator, IHandlerFactory
{
    private readonly Dictionary<Type, string> commandHandlerMap = new();
    private readonly Dictionary<string, object> handlerStorage = new();

    public string Name(IMessage message)
    {
        return commandHandlerMap.First(pair => pair.Key == message.GetType()).Value;
    }

    public object Create(string handlerName)
    {
        return handlerStorage.First(pair => pair.Key.Equals(handlerName)).Value;
    }

    public void Map(Type message, object handler)
    {
        var handlerName = handler.GetType().FullName!;
        
        commandHandlerMap.Add(message, handlerName);
        handlerStorage.Add(handlerName, handler);
    }

    public void Clear()
    {
        commandHandlerMap.Clear();
        handlerStorage.Clear();
    }
}
