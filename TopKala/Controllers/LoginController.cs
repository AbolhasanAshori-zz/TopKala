using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TopKala.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("/" + nameof(Register))]
        public IActionResult Register()
        {
            return View();
        }

        [Route("/" + nameof(Welcome))]
        public IActionResult Welcome()
        {
            return View();
        }

        [Route("/" + nameof(Password))]
        public IActionResult Password()
        {
            return View();
        }
    }
}