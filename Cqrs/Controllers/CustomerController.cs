using System.Web.Mvc;

namespace Cqrs.Controllers
{
    public class CustomerController : Controller
    {
        public CustomerController()
        {

        }

        [Route("")]
        [Route("customers")]
        public ActionResult Index()
        {
            return View();
        }
    }
}