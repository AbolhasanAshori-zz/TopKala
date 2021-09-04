using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TopKala.Models.ViewModels;
using TopKala.Services.Interfaces;

namespace TopKala.VIewComponents
{
    public class ProfileInfoViewComponent : ViewComponent
    {
        private readonly ISignInManager _signInManager;

        public ProfileInfoViewComponent(ISignInManager signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int colSize = 12)
        {
            var user = _signInManager.GetUser();
            var profileInfoVM = new ProfileInfoVM()
            {
                Fullname = (!string.IsNullOrEmpty(user.FirstName) ? user.FirstName : "") + " " + (!string.IsNullOrEmpty(user.LastName) ? user.LastName : ""),
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IdNumber = user.IdNumber,
                NewsletterSubscript = user.IsNewsletterSubscripted,
                CardNumber = user.CardNumber,
                ColSize = colSize
            };
            return View(profileInfoVM);
        }
    }
}