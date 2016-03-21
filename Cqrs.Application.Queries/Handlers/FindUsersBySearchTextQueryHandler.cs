using Cqrs.Application.Domain;
using Cqrs.Connections;
using Cqrs.Querying;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cqrs.Application.Queries.Handlers
{
    public class FindUsersBySearchTextQueryHandler : IQueryHandler<FindUsersBySearchTextQuery, IEnumerable<User>>
    {
        private readonly IConnectionFactory factory;

        public FindUsersBySearchTextQueryHandler(IConnectionFactory factory)
        {
            this.factory = factory;
        }

        public IEnumerable<User> Handle(FindUsersBySearchTextQuery query)
        {
            using (var con = factory.CreateConnection())
            {
                var sql = "SELECT * FROM Users WHERE Name LIKE '%' + @Name + '%'";
                if (!query.IncludeInactiveUsers)
                    sql += " AND Active = 1";

                return con.Query<User>(sql, new { Name = query.SearchText });
            }
        }
    }

    public class FindUsersBySearchTextAsyncQueryHandler : IAsyncQueryHandler<FindUsersBySearchTextQuery, IEnumerable<User>>
    {
        private readonly IConnectionFactory factory;

        public FindUsersBySearchTextAsyncQueryHandler(IConnectionFactory factory)
        {
            this.factory = factory;
        }

        public async Task<IEnumerable<User>> Handle(FindUsersBySearchTextQuery query)
        {
            using (var con = factory.CreateConnection())
            {
                var sql = "SELECT * FROM Users WHERE Name LIKE '%' + @Name + '%'";
                if (!query.IncludeInactiveUsers)
                    sql += " AND Active = 1";
                return await con.QueryAsync<User>(sql, new { Name = query.SearchText });
            }
        }
    }
}