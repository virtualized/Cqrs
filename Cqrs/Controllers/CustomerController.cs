using Cqrs.Application.Query.Handlers;
using Cqrs.Application.Query.Queries;
using Cqrs.Infrastructure.Dto;
using Cqrs.ViewModels;
using System.Web.Mvc;

namespace Cqrs.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IQueryHandler<CustomersDto, GetAllCustomersQuery> getCustomersQueryHandler;

        public CustomerController(IQueryHandler<CustomersDto, GetAllCustomersQuery> getCustomersQueryHandler)
        {
            this.getCustomersQueryHandler = getCustomersQueryHandler;
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
    }
}