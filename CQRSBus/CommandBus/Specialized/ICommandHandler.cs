using CQRSBus.Specialized;

namespace CQRSBus.CommandBus.Specialized;

public interface ICommandHandler<in TX, out TResult> : IHandler<TX, TResult> where TX : ICommand
{
}