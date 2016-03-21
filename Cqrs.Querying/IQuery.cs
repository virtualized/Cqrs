using System.Threading.Tasks;

namespace Cqrs.Querying
{
    public interface IQuery<TResult>
    {
    }

    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }

    public interface IAsyncQueryHandler<TQuery, TResult>
    {
        Task<TResult> Handle(TQuery query);
    }
}