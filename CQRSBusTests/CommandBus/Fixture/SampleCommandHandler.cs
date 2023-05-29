using CQRSBus.CommandBus.Specialized;

namespace CQRSBusTests.CommandBus.Fixture;

public class SampleCommandHandler : ICommandHandler<SampleCommand, string>
{
    public string Handle(SampleCommand message)
    {
        return message.Input;
    }
}