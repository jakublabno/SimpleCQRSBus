using CQRSBusTests.CommandBus;
using CQRSBusTests.CommandBus.Fixture;

namespace CQRSBusTests;

public class CommandBusTest : CommandBusTestCase
{
    [Test]
    public void Any()
    {
        AddExecutorMiddleware();
        var input = "any input";
        var command = new SampleCommand(input);
        
        var result = CommandBus().Handle<string>(command);

        Assert.AreEqual(input, result);
    }
}
