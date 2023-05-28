using CQRSBus.CommandBus.Specialized;

namespace CQRSBusTests.CommandBus.Fixture;

public class SampleCommand : ICommand
{
    public readonly string Input;

    public SampleCommand(string input)
    {
        Input = input;
    }
}
