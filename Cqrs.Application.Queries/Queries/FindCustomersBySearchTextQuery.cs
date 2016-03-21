using Cqrs.Application.Domain;
using Cqrs.Querying;
using System.Collections.Generic;

namespace Cqrs.Application.Queries
{
    public class FindUsersBySearchTextQuery : IQuery<IEnumerable<User>>
    {
        public string SearchText { get; set; }
        public bool IncludeInactiveUsers { get; set; }
    }
}