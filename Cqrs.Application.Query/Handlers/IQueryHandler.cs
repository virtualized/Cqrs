using Cqrs.Infrastructure.Dto;
using System.Threading.Tasks;

namespace Cqrs.Application.Query.Handlers
{
    public interface IQueryHandler<out TDto, in TQuery>
        where TDto : Dto where TQuery : Queries.Query<TDto>
    {
        TDto Handle(TQuery query);
    }

    public interface IAsyncQueryHandler<in TQuery, TResult>
        where TQuery : Queries.Query<TResult> where TResult : Dto
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}