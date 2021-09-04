using System;
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
            if (ReturnUrl != "/")
            {
                TempData["Warning"] = "برای مشاهده این صفحه ابتدا باید وارد سایت شوید";
            }

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
                if (_userManager.VerifyPhoneNumber(loginVM.UserEmailPhone))
                    user = _userManager.ValidatePasswordWithPhoneNumber(loginVM.UserEmailPhone, loginVM.Password, loginVM.RememberMe);
                else if (_userManager.VerifyEmail(loginVM.UserEmailPhone))
                    user = _userManager.ValidatePasswordWithEmail(loginVM.UserEmailPhone, loginVM.Password, loginVM.RememberMe);
                else
                    user = _userManager.ValidatePasswordWithUsername(loginVM.UserEmailPhone, loginVM.Password, loginVM.RememberMe);
            }
            catch (UserNotFoundException)
            {
                TempData["Error"] = "رمز عبور یا ایمیل یا شماره موبایل اشتباه می باشد";
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
            
            User user = new User();

            try
            {
                if (_userManager.CheckUsername(registerVM.Username))
                    user.Username = registerVM.Username;

                if (_userManager.VerifyPhoneNumber(registerVM.EmailPhone) 
                        && _userManager.CheckPhoneNumber(registerVM.EmailPhone))
                    user.PhoneNumber = registerVM.EmailPhone;
                else if(_userManager.VerifyEmail(registerVM.EmailPhone) 
                        && _userManager.CheckEmail(registerVM.EmailPhone))
                    user.Email = registerVM.EmailPhone;
                else
                {
                    ModelState.AddModelError(nameof(registerVM.EmailPhone),
                        "لطفا ایمیل یا شماره موبایل خود را به درستی وارد نمایید");
                    return View(registerVM);
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View(registerVM);
            }

            _userManager.Create(user, registerVM.Password);
            _signInManager.SignIn(user.Username, registerVM.Password, false);
            return View("Welcome", registerVM.ReturnUrl);
        }    
    }
}