using System;

namespace Cqrs.Infrastructure.Dto
{
    public class OrderDto : Dto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public DateTime PlacedOn { get; set; }
        public int CustomerId { get; set; }
    }
}