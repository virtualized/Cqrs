using Cqrs.Infrastructure.Dto;

namespace Cqrs.Application.Query.Queries
{
    public class GetCustomersByNameQuery : PagedQuery<CustomersDto>
    {
        public string Name { get; set; }
    }
}