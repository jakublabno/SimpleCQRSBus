namespace CQRSBus.Specialized;

public interface IHandler<in T, out TResult> where T : IMessage
{
    public TResult Handle(T message);
}