using System.Collections.Generic;

namespace Cqrs.Infrastructure.Dto
{
    public class CustomerDto : Dto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public IEnumerable<OrderDto> Orders { get; set; }
    }
}