using Cqrs.Infrastructure.Dto;

namespace Cqrs.Application.Query.Queries
{
    public class GetAllCustomersQuery : PagedQuery<CustomersDto>
    {
        public int CustomerId { get; set; }
    }
}