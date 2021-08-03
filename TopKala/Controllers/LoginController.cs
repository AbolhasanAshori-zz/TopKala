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
            if (!ModelState.IsValid)
                return View(new LoginVM(){ ReturnUrl = loginVM.ReturnUrl });

            User user = null;
            try 
            {
                if (_userManager.VerifyPhoneNumber(loginVM.User_Email_Phone))
                    user = _userManager.ValidatePasswordWithPhoneNumber(loginVM.User_Email_Phone, loginVM.Password, loginVM.RememberMe);
                else if (_userManager.VerifyEmail(loginVM.User_Email_Phone))
                    user = _userManager.ValidatePasswordWithEmail(loginVM.User_Email_Phone, loginVM.Password, loginVM.RememberMe);
                else
                    user = _userManager.ValidatePasswordWithUsername(loginVM.User_Email_Phone, loginVM.Password, loginVM.RememberMe);
            }
            catch (UserNotFoundException)
            {
                ViewBag.Message = "رمز عبور یا ایمیل یا شماره موبایل اشتباه می باشد";
                return View(loginVM);
            }

            _signInManager.SignIn(user, loginVM.RememberMe);
            return LocalRedirect(loginVM.ReturnUrl);
        }

        public IActionResult LogOut()
        {
            _signInManager.SignOut();
            return LocalRedirect("/");
        }

        [Route("/" + nameof(Register))]
        public IActionResult Register(string ReturnUrl = "/")
        {
            RegisterVM registerVM = new RegisterVM()
            {
                ReturnUrl = ReturnUrl
            };
            return View(registerVM);
        }

        [HttpPost("/" + nameof(Register))]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) 
                return View(registerVM);

            var user = new User()
            {
                Role = _unitOfWork.UserRole.GetFirstOrDefault(ur => ur.Name == "Normal"),
                IsActive = true
            };

            try
            {
                if (_userManager.CheckUsername(registerVM.Username))
                    user.Username = registerVM.Username;

                if (_userManager.VerifyPhoneNumber(registerVM.Email_Phone) 
                        && _userManager.CheckPhoneNumber(registerVM.Email_Phone))
                    user.PhoneNumber = registerVM.Email_Phone;
                else if(_userManager.VerifyEmail(registerVM.Email_Phone) 
                        && _userManager.CheckEmail(registerVM.Email_Phone))
                    user.Email = registerVM.Email_Phone;
                else
                {
                    ModelState.AddModelError(nameof(registerVM.Email_Phone),
                        "لطفا ایمیل یا شماره موبایل خود را به درستی وارد نمایید");
                    return View(registerVM);
                }
            }
            catch (UserExistException ex)
            {
                ViewBag.Message = ex.Message;
                return View(registerVM);
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerVM.Password);
            _unitOfWork.User.Add(user);
            _unitOfWork.Save();

            _signInManager.SignIn(user.Username, registerVM.Password, false);
            return View("Welcome", registerVM.ReturnUrl);
        }    

        [Route("/" + nameof(Password))]
        [Authorize]
        public IActionResult Password()
        {
            return View();
        }

        [HttpPost("/" + nameof(Password))]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Password(PasswordVM passwordVM)
        {
            if (!ModelState.IsValid)
                return View();

            if (passwordVM.OldPassword == passwordVM.NewPassword)
            {
                ViewBag.Message = "رمز عبور نمی تواند تکراری باشد";
                return View();
            }

            var username = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var user = _unitOfWork.User.GetFirstOrDefault(u => u.Username == username);

            if (!BCrypt.Net.BCrypt.Verify(passwordVM.OldPassword, user.PasswordHash))
            {
                ViewBag.Message = "رمز عبور قبلی نادرست می باشد";
                return View();
            }
                
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(passwordVM.NewPassword);
            _unitOfWork.Save();
            return RedirectToAction("Index", "Profile");
        }
    }
}