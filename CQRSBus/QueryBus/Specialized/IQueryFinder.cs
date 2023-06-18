using CQRSBus.Specialized;

namespace CQRSBus.QueryBus.Specialized;

public interface IQueryFinder<in TX, out TResult> : IHandler<TX, TResult> where TX : IQuery
{
}
