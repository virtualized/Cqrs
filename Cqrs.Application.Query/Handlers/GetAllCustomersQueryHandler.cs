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

    public class GetCustomersByNameQueryHandler : IQueryHandler<CustomersDto, GetCustomersByNameQuery>
    {
        private readonly IDapperConnectionFactory dapperConnectionFactory;

        public GetCustomersByNameQueryHandler(IDapperConnectionFactory dapperConnectionFactory)
        {
            this.dapperConnectionFactory = dapperConnectionFactory;
        }

        public CustomersDto Handle(GetCustomersByNameQuery query)
        {
            using (var connection = dapperConnectionFactory.CreateConnection())
            {
                connection.Open();
                var customers = connection.Query<CustomerDto>("SELECT * FROM Customers WHERE Name LIKE @Name", new { Name = query.Name });
                return new CustomersDto { Customers = customers };
            }
        }
    }
}