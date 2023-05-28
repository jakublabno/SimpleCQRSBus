using CQRSBus.CommandBus.Specialized;
using CQRSBus.Locator;
using CQRSBus.Specialized;

namespace CQRSBusTests.CommandBus.Double;

public class NameInflectorLocator : IHandlerLocator<ICommand>
{
    private readonly string stringAddition;

    public NameInflectorLocator(string stringAddition)
    {
        this.stringAddition = stringAddition;
    }

    public Type Name(IMessage message)
    {
        var handlerName = message.GetType().FullName + stringAddition;

        return Type.GetType(handlerName)!;
    }
}
