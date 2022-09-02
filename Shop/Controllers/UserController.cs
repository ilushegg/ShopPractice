using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
