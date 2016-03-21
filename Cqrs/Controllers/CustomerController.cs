using Cqrs.Application.Query.Handlers;
using Cqrs.Application.Query.Queries;
using Cqrs.Infrastructure.Dto;
using Cqrs.ViewModels;
using System.Web.Mvc;

namespace Cqrs.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IQueryHandler<CustomersDto, GetCustomersByNameQuery> getCustomersByNameQueryHandler;
        private readonly IQueryHandler<CustomersDto, GetAllCustomersQuery> getCustomersQueryHandler;

        public CustomerController(
            IQueryHandler<CustomersDto, GetAllCustomersQuery> getCustomersQueryHandler,
            IQueryHandler<CustomersDto, GetCustomersByNameQuery> getCustomersByNameQueryHandler)
        {
            this.getCustomersQueryHandler = getCustomersQueryHandler;
            this.getCustomersByNameQueryHandler = getCustomersByNameQueryHandler;
        }

        [Route("")]
        [Route("customers")]
        [HttpGet]
        public ActionResult Index()
        {
            var getCustomersQuery = new GetAllCustomersQuery { Page = 1, ResultsPerPage = 10 };
            var getCustomersDto = this.getCustomersQueryHandler.Handle(getCustomersQuery);
            return View(new CustomersViewModel { Data = getCustomersDto });
        }

        [Route("customers/{name}")]
        [HttpGet]
        public ActionResult Index(string name)
        {
            var getCustomersByNameQuery = new GetCustomersByNameQuery { Name = name, Page = 1, ResultsPerPage = 10 };
            var getCustomersDto = getCustomersByNameQueryHandler.Handle(getCustomersByNameQuery);
            return View(new CustomersViewModel { Data = getCustomersDto });
        }
    }
}