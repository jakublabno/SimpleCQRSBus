using CQRSBus.Specialized;

namespace CQRSBus.Middleware;

public abstract class MiddlewareCollection<T, TS>
    where T : IMiddleware<TS>
    where TS : IMessage
{
    private readonly LinkedList<T> collection = new ();

    public MiddlewareCollection<T, TS> Add(T middleware)
    {
        collection.AddLast(middleware);

        return this;
    }

    public void Clear() => collection.Clear();

    public Stack<T> ToStack()
    {
        return new Stack<T>(collection);
    }
}
