using System.Collections.Generic;

namespace Cqrs.Infrastructure.Dto
{
    public class CustomersDto : Dto
    {
        public IEnumerable<CustomerDto> Customers { get; set; }
    }
}