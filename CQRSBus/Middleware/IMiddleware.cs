using CQRSBus.Specialized;

namespace CQRSBus.Middleware;

public interface IMiddleware<in T> where T : IMessage
{
    dynamic? Execute(T message, Func<IMessage, dynamic>? callable);
}
