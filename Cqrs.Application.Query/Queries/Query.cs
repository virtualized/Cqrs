using Cqrs.Infrastructure.Dto;

namespace Cqrs.Application.Query.Queries
{
    public abstract class Query<TResult> : Dto where TResult : Dto
    {
    }
}