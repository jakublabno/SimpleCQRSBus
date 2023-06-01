using CQRSBus.Specialized;

namespace CQRSBus;

internal interface IBus<in T> where T : IMessage
{
    TResult Handle<TResult>(T message);
}
