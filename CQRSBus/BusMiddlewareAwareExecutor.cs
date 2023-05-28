using CQRSBus.Middleware;
using CQRSBus.Specialized;

namespace CQRSBus;

public class BusMiddlewareAwareExecutor<T, TS, TU>
    where T : IMessage
    where TS : IMiddleware<T>
    where TU : MiddlewareCollection<TS, T>
{
    private readonly Stack<TS> middlewareStack;

    public BusMiddlewareAwareExecutor(TU middlewareCollection)
    {
        middlewareStack = middlewareCollection.ToStack();
    }

    public dynamic? Execute(T message)
    {
        var localMiddlewareStack = new Stack<TS>(middlewareStack);

        Func<IMessage, dynamic>? lastCallable = null;

        while (localMiddlewareStack.Count > 0)
        {
            var middleware = localMiddlewareStack.Pop();
            var localCallable = middleware.Execute(message, lastCallable);

            if (localCallable is not Delegate)
            {
                return localCallable;
            }

            lastCallable = localCallable;
        }

        return lastCallable?.Invoke(message);
    }
}
