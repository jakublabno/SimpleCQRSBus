using CQRSBus.CommandBus.Builder;
using CQRSBusTests.CommandBus.Fixture;
using CQRSBusTests.Helper;

namespace CQRSBusTests.QueryBus;

public class CommandBusBuilderTest
{
    [Test]
    public void Builder()
    {
        var handlerFactory = new InMemoryHandlerManager();
        handlerFactory.Map(typeof(SampleCommand), new SampleCommandHandler());
        
        var bus = new CommandBusBuilder()
            .SetHandlerFactory(handlerFactory)
            .SetHandlerLocator(handlerFactory)
            .AddExecutionMiddleware()
            .Build();

        bus.Handle<string>(new SampleCommand("test"));
    }
}
