using CQRSBus.CommandBus.Middleware;
using CQRSBus.CommandBus.Specialized;
using CQRSBus.Specialized;
using CQRSBusTests.CommandBus.Fixture;

namespace CQRSBusTests.CommandBus;

public class MiddlewareTest : CommandBusTestCase
{
    [Test]
    public void execute_given_middleware()
    {
        var firstMiddleware = new FirstMiddleware();
        AddMiddleWare(firstMiddleware);

        CommandBus().Handle(new DummyCommand());

        var wasMiddlewareExecuted = firstMiddleware.Executed;
        Assert.IsTrue(wasMiddlewareExecuted);
    }

    [Test]
    public void ignore_following_middlewares_when_output_is_not_callable()
    {
        var firstMiddleware = new FirstMiddleware();
        var secondMiddleware = new SecondMiddlewareWithOutput();
        AddMiddleWare(firstMiddleware, secondMiddleware);

        CommandBus().Handle(new DummyCommand());

        var wasMiddlewareExecuted = firstMiddleware.Executed;
        Assert.IsTrue(wasMiddlewareExecuted);

        wasMiddlewareExecuted = secondMiddleware.Executed;
        Assert.IsFalse(wasMiddlewareExecuted);
    }

    [Test]
    public void execute_middleware_in_chain()
    {
        var firstMiddleware = new SecondMiddlewareWithOutput();
        var secondMiddleware = new FirstMiddleware();
        AddMiddleWare(firstMiddleware, secondMiddleware);

        CommandBus().Handle(new DummyCommand());

        var wasMiddlewareExecuted = firstMiddleware.Executed;
        Assert.IsTrue(wasMiddlewareExecuted);

        wasMiddlewareExecuted = secondMiddleware.Executed;
        Assert.IsTrue(wasMiddlewareExecuted);
    }

    [Test]
    public void middleware_can_transform_messages()
    {
        var originMessage = "not transformed message";
        var newMessage = "transformed property";
        var transformingMiddleware = new TransformingMiddlewareWithOutput(newMessage);
        AddMiddleWare(transformingMiddleware, new FirstMiddleware());
        var command = new SampleCommand(originMessage);

        CommandBus().Handle(command);

        var expectedInput = newMessage;
        Assert.AreEqual(expectedInput, command.Input);
    }

    [Test]
    public void propagate_transformed_message_in_chain()
    {
        var originMessage = "not transformed message";
        var newMessage = "transformed property";
        var transformingMiddleware = new TransformingMiddlewareWithOutput(newMessage);
        var spyMiddleware = new SpyMiddleware();
        AddMiddleWare(transformingMiddleware, spyMiddleware);
        var command = new SampleCommand(originMessage);

        CommandBus().Handle(command);

        var expectedInput = ((SampleCommand)spyMiddleware.ExecutedCommand).Input;
        Assert.AreEqual(newMessage, expectedInput);
    }

    private class FirstMiddleware : ICommandBusMiddleware
    {
        public bool Executed;

        public dynamic? Execute(ICommand message, Func<IMessage, dynamic>? callable)
        {
            Executed = true;

            return default;
        }
    }

    private class SecondMiddlewareWithOutput : ICommandBusMiddleware
    {
        public bool Executed;

        public dynamic? Execute(ICommand message, Func<IMessage, dynamic>? callable)
        {
            Executed = true;

            var del = (IMessage message) => callable(message);

            return del;
        }
    }

    private class TransformingMiddlewareWithOutput : ICommandBusMiddleware
    {
        private readonly string transformTo;

        public TransformingMiddlewareWithOutput(string transformTo)
        {
            this.transformTo = transformTo;
        }

        public dynamic? Execute(ICommand message, Func<IMessage, dynamic>? callable)
        {
            var msg = (SampleCommand)message;
            msg.Input = transformTo;

            var del = (IMessage message) => callable(message);

            return del;
        }
    }

    private class SpyMiddleware : ICommandBusMiddleware
    {
        public ICommand ExecutedCommand;

        public dynamic? Execute(ICommand message, Func<IMessage, dynamic>? callable)
        {
            ExecutedCommand = message;

            return default;
        }
    }
}