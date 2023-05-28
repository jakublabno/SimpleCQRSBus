using CQRSBus.Specialized;

namespace CQRSBus.Middleware;

public abstract class MiddlewareCollection<T, TS> : LinkedList<T>
    where T : IMiddleware<TS>
    where TS : IMessage
{
    public Stack<T> ToStack()
    {
        return new Stack<T>(this);
    }
}