using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TopKala.DataAccess.Repository.IRepository;
using TopKala.Models.ViewModels;
using TopKala.Services.Interfaces;

namespace TopKala.VIewComponents
{
    public class ProfileFavoriteViewComponent : ViewComponent
    {
        private readonly ISignInManager _signInManager;
        private readonly IUnitOfWork _unitOfWork;

        public ProfileFavoriteViewComponent(ISignInManager signInManager, IUnitOfWork unitOfWork)
        {
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync(int colSize = 12)
        {
            // TODO: Complete User Favorite Product View Component
            var user = _signInManager.GetUser();
            return View();
        }
    }
}