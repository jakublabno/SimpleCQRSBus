using CQRSBusTests.QueryBus.Fixture;

namespace CQRSBusTests.QueryBus;

public class QueryBusTest : QueryBusTestCase
{
    [Test]
    public void handle_with_return_type_declared()
    {
        AddExecutorMiddleware();
        var input = "any input";
        var command = new SampleQuery(input);

        var result = QueryBus().Handle<string>(command);

        var expectedResult = input;
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}
