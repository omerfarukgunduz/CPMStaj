using Microsoft.AspNetCore.Mvc;

namespace Portal.Web.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
