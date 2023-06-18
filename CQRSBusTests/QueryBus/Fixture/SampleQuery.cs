using CQRSBus.QueryBus.Specialized;

namespace CQRSBusTests.QueryBus.Fixture;

public class SampleQuery : IQuery
{
    public string Input;

    public SampleQuery(string input)
    {
        Input = input;
    }
}
