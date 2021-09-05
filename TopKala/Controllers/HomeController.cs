using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TopKala.Models.ViewModels;

namespace TopKala.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        [Route("/Search")]
        public IActionResult Search()
        {
            return View();
        }

        [Route("/Search")]
        public IActionResult Search([FromQuery(Name = "s")]string searchString)
        {
            return View(new SearchVM(searchString));
        }

        [Route("/Search")]
        public IActionResult Search(SearchVM searchVM)
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
