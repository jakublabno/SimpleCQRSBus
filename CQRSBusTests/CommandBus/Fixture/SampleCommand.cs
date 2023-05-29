using CQRSBus.CommandBus.Specialized;

namespace CQRSBusTests.CommandBus.Fixture;

public class SampleCommand : ICommand
{
    public string Input;

    public SampleCommand(string input)
    {
        Input = input;
    }
}