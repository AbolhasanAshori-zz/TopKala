using Microsoft.AspNetCore.Mvc;

namespace TopKala.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Empty()
        {
            return View();
        }
    }
}