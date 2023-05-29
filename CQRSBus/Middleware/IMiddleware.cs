using CQRSBus.Specialized;

namespace CQRSBus.Middleware;

public interface IMiddleware<T> where T : IMessage
{
    dynamic? Execute(T message, Func<IMessage, dynamic>? callable);
}