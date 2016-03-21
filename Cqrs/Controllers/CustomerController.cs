//using Cqrs.Application.Query.Handlers;
//using Cqrs.Application.Query.Queries;
//using Cqrs.Infrastructure.Dto;
//using Cqrs.ViewModels;
//using System.Threading.Tasks;
//using System.Web.Mvc;

//namespace Cqrs.Controllers
//{
//    public class CustomerController : Controller
//    {
//        private readonly IAsyncQueryHandler<GetCustomersByNameQuery, CustomersDto> getCustomersByNameAsyncQueryHandler;
//        private readonly IQueryHandler<CustomersDto, GetAllCustomersQuery> getCustomersQueryHandler;

//        public CustomerController(
//            IQueryHandler<CustomersDto, GetAllCustomersQuery> getCustomersQueryHandler,
//            IAsyncQueryHandler<GetCustomersByNameQuery, CustomersDto> getCustomersByNameAsyncQueryHandler)
//        {
//            this.getCustomersQueryHandler = getCustomersQueryHandler;
//            this.getCustomersByNameAsyncQueryHandler = getCustomersByNameAsyncQueryHandler;
//        }

//        [Route("")]
//        [Route("customers")]
//        [HttpGet]
//        public ActionResult Index()
//        {
//            var getCustomersQuery = new GetAllCustomersQuery { Page = 1, ResultsPerPage = 10 };
//            var getCustomersDto = this.getCustomersQueryHandler.Handle(getCustomersQuery);
//            return View(new CustomersViewModel { Data = getCustomersDto });
//        }

//        [Route("customers/{name}")]
//        [HttpGet]
//        public async Task<ActionResult> Index(string name)
//        {
//            var getCustomersByNameQuery = new GetCustomersByNameQuery { Name = name, Page = 1, ResultsPerPage = 10 };
//            var getCustomersDto = await getCustomersByNameAsyncQueryHandler.HandleAsync(getCustomersByNameQuery);
//            return View(new CustomersViewModel { Data = getCustomersDto });
//            //var getCustomersByNameQuery = new GetCustomersByNameQuery { Name = name, Page = 1, ResultsPerPage = 10 };
//            //var getCustomersDto = getCustomersByNameQueryHandler.Handle(getCustomersByNameQuery);
//            //return View(new CustomersViewModel { Data = getCustomersDto });
//        }
//    }
//}