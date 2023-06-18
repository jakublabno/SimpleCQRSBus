using CQRSBus.CommandBus.Middleware;
using CQRSBus.CommandBus.Specialized;
using CQRSBus.QueryBus.Middleware;
using CQRSBus.QueryBus.Specialized;
using CQRSBus.Specialized;
using CQRSBusTests.CommandBus.Fixture;
using CQRSBusTests.QueryBus.Fixture;

namespace CQRSBusTests.QueryBus;

public class MiddlewareTest : QueryBusTestCase
{
    [Test]
    public void execute_given_middleware()
    {
        var firstMiddleware = new FirstMiddleware();
        AddMiddleWare(firstMiddleware);

        QueryBus().Handle<string>(new DummyQuery());

        var wasMiddlewareExecuted = firstMiddleware.Executed;
        Assert.IsTrue(wasMiddlewareExecuted);
    }

    [Test]
    public void ignore_following_middlewares_when_output_is_not_callable()
    {
        var firstMiddleware = new FirstMiddleware();
        var secondMiddleware = new SecondMiddlewareWithOutput();
        AddMiddleWare(firstMiddleware, secondMiddleware);

        QueryBus().Handle<string>(new DummyQuery());

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

        QueryBus().Handle<string>(new DummyQuery());

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
        var query = new SampleQuery(originMessage);

        QueryBus().Handle<string>(query);

        var expectedInput = newMessage;
        Assert.AreEqual(expectedInput, query.Input);
    }

    [Test]
    public void propagate_transformed_message_in_chain()
    {
        var originMessage = "not transformed message";
        var newMessage = "transformed property";
        var transformingMiddleware = new TransformingMiddlewareWithOutput(newMessage);
        var spyMiddleware = new SpyMiddleware();
        AddMiddleWare(transformingMiddleware, spyMiddleware);
        var query = new SampleQuery(originMessage);

        QueryBus().Handle<string>(query);

        var expectedInput = ((SampleQuery)spyMiddleware.ExecutedQuery).Input;
        Assert.AreEqual(newMessage, expectedInput);
    }

    private class FirstMiddleware : IQueryBusMiddleware
    {
        public bool Executed;

        public dynamic? Execute(IQuery message, Func<IMessage, dynamic>? callable)
        {
            Executed = true;

            return default;
        }
    }

    private class SecondMiddlewareWithOutput : IQueryBusMiddleware
    {
        public bool Executed;

        public dynamic? Execute(IQuery message, Func<IMessage, dynamic>? callable)
        {
            Executed = true;

            var del = (IMessage message) => callable(message);

            return del;
        }
    }

    private class TransformingMiddlewareWithOutput : IQueryBusMiddleware
    {
        private readonly string transformTo;

        public TransformingMiddlewareWithOutput(string transformTo)
        {
            this.transformTo = transformTo;
        }

        public dynamic? Execute(IQuery message, Func<IMessage, dynamic>? callable)
        {
            var msg = (SampleQuery)message;
            msg.Input = transformTo;

            var del = (IMessage message) => callable(message);

            return del;
        }
    }

    private class SpyMiddleware : IQueryBusMiddleware
    {
        public IQuery ExecutedQuery;

        public dynamic? Execute(IQuery message, Func<IMessage, dynamic>? callable)
        {
            ExecutedQuery = message;

            return default;
        }
    }
}
