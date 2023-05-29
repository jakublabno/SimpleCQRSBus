using CQRSBus.Specialized;

namespace CQRSBus.Locator;

public class NameInflectorLocator : IHandlerLocator
{
    private readonly INameInflectorStrategy strategy;

    public NameInflectorLocator(INameInflectorStrategy strategy)
    {
        this.strategy = strategy;
    }

    public string Name(IMessage message)
    {
        var commandName = message.GetType().FullName;

        return strategy.Inflect(commandName);
    }
}