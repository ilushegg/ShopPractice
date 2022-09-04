using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Shop.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet]
        public IActionResult Products() => View();

        
    }
}
