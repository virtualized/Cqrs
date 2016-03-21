using Cqrs.Infrastructure.Dto;

namespace Cqrs.Application.Query.Queries
{
    public class PagedQuery<TResult> : Query<TResult>
        where TResult : Dto
    {
        public int Page { get; set; }
        public int ResultsPerPage { get; set; }
    }
}