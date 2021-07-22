using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TopKala.DataAccess.Repository.IRepository;
using TopKala.Exceptions;
using TopKala.Models;
using TopKala.Models.ViewModels;
using TopKala.Services.Interfaces;

namespace TopKala.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserManager _userManager;
        private readonly ISignInManager _signInManager;
        public LoginController(IUnitOfWork unitOfWork, IUserManager userManager, ISignInManager signInManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index(string ReturnUrl = "/")
        {
            LoginVM loginVM = new LoginVM()
            {
                ReturnUrl = ReturnUrl
            };
            return View(loginVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                try 
                {
                    User user = null;
                    if (_userManager.VerifyPhoneNumber(loginVM.User_Email_Phone))
                        user = _userManager.ValidatePasswordWithPhoneNumber(loginVM.User_Email_Phone, loginVM.Password, loginVM.RememberMe);
                    else if (_userManager.VerifyEmail(loginVM.User_Email_Phone))
                        user = _userManager.ValidatePasswordWithEmail(loginVM.User_Email_Phone, loginVM.Password, loginVM.RememberMe);
                    else
                        user = _userManager.ValidatePasswordWithUsername(loginVM.User_Email_Phone, loginVM.Password, loginVM.RememberMe);
                    
                    _signInManager.SignIn(user, loginVM.RememberMe);
                    return LocalRedirect(loginVM.ReturnUrl);   
                }
                catch (UserNotFoundException)
                {
                    ViewBag.Message = "رمز عبور یا ایمیل یا شماره موبایل اشتباه می باشد";
                    return View(loginVM);
                }
            }
            return View(new LoginVM(){ ReturnUrl = loginVM.ReturnUrl });
        }

        public IActionResult LogOut()
        {
            _signInManager.SignOut();
            return LocalRedirect("/");
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