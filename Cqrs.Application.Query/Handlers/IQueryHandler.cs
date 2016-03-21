using Cqrs.Infrastructure.Dto;

namespace Cqrs.Application.Query.Handlers
{
    public interface IQueryHandler<out TDto, in TQuery>
        where TDto : Dto where TQuery : Queries.Query<TDto>
    {
        TDto Handle(TQuery query);
    }
}