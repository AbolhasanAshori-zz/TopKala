using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TopKala.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Orders()
        {
            return View();
        }

        public IActionResult OrdersReturn()
        {
            return View();
        }

        public IActionResult Favorites()
        {
            return View();
        }

        public IActionResult InfoPersonal()
        {
            return View();
        }

        public IActionResult InfoAdditional()
        {
            return View();
        }
    }
}