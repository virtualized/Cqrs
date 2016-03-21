using Cqrs.Application.Domain;
using Cqrs.Application.Queries;
using Cqrs.Querying;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cqrs.Controllers
{
    public class UserController : Controller
    {
        private readonly IAsyncQueryHandler<FindUsersBySearchTextQuery, IEnumerable<User>> asyncHandler;
        private readonly IQueryHandler<FindUsersBySearchTextQuery, IEnumerable<User>> handler;

        public UserController(
            IQueryHandler<FindUsersBySearchTextQuery, IEnumerable<User>> handler,
            IAsyncQueryHandler<FindUsersBySearchTextQuery, IEnumerable<User>> asyncHandler)
        {
            this.handler = handler;
            this.asyncHandler = asyncHandler;
        }

        [Route("search/{searchText}")]
        [HttpGet]
        public ActionResult Search(string searchText)
        {
            var query = new FindUsersBySearchTextQuery
            {
                SearchText = searchText,
                IncludeInactiveUsers = true
            };
            return View(handler.Handle(query));
        }

        [Route("searchasync/{searchText}")]
        [HttpGet]
        public async Task<ActionResult> SearchAsync(string searchText)
        {
            var query = new FindUsersBySearchTextQuery { SearchText = searchText, IncludeInactiveUsers = true };
            var users = await asyncHandler.Handle(query);
            return View(users);
        }
    }
}