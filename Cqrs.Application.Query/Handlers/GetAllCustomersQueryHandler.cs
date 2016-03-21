using Cqrs.Application.Query.Queries;
using Cqrs.Infrastructure.Dapper;
using Cqrs.Infrastructure.Dto;

namespace Cqrs.Application.Query.Handlers
{
    public class GetAllCustomersQueryHandler : IQueryHandler<CustomersDto, GetAllCustomersQuery>
    {
        private readonly IDapperConnectionFactory dapperConnectionFactory;

        public GetAllCustomersQueryHandler(IDapperConnectionFactory dapperConnectionFactory)
        {
            this.dapperConnectionFactory = dapperConnectionFactory;
        }

        public CustomersDto Handle(GetAllCustomersQuery query)
        {
            using (var connection = dapperConnectionFactory.CreateConnection())
            {
                connection.Open();
                var customers = connection.Query<CustomerDto>("SELECT * FROM Customers");
                return new CustomersDto { Customers = customers };
            }
        }
    }
}