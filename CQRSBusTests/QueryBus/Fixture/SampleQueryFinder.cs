using CQRSBus.QueryBus.Specialized;

namespace CQRSBusTests.QueryBus.Fixture;

public class SampleQueryFinder : IQueryFinder<SampleQuery, string>
{
    public string Handle(SampleQuery message)
    {
        return message.Input;
    }
}
