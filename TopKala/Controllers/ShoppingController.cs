using Microsoft.AspNetCore.Mvc;

namespace TopKala.Controllers
{
    public class ShoppingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Payment()
        {
            return View();
        }

        public IActionResult Complete()
        {
            return View();
        }

        public IActionResult Failed()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }
    }
}