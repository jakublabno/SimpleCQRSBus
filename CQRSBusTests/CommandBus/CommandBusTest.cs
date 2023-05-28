using CQRSBusTests.CommandBus;
using CQRSBusTests.CommandBus.Fixture;

namespace CQRSBusTests;

public class CommandBusTest : CommandBusTestCase
{
    [Test]
    public void handle_with_return_type_declared()
    {
        AddExecutorMiddleware();
        var input = "any input";
        var command = new SampleCommand(input);
        
        var result = CommandBus().Handle<string>(command);

        var expectedResult = input;
        Assert.AreEqual(expectedResult, result);
    }
    
    [Test]
    public void handle_without_return()
    {
        AddExecutorMiddleware();
        var input = "any input";
        var command = new SampleCommand(input);
        
        CommandBus().Handle(command);

        Assert.IsTrue(true);
    }
}
