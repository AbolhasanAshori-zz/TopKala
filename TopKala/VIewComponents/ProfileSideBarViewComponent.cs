using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TopKala.Enums;
using TopKala.Models.ViewModels;
using TopKala.Services.Interfaces;

namespace TopKala.VIewComponents
{
    public class ProfileSideBarViewComponent : ViewComponent
    {
        private readonly ISignInManager _signInManager;
        private readonly IFileService _fileService;

        public ProfileSideBarViewComponent(ISignInManager signInManager, IFileService fileService)
        {
            _signInManager = signInManager;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = _signInManager.GetUser();
            string image = null;

            if (user.IsImageUploaded)
                image = _fileService.GetPath(user.Image, UploadType.Profile);
            else
                image = _fileService.GetPath(user.Image, FileType.Profile);

            var profileSideVM = new ProfileSideVM()
            {
                Username = user.Username,
                Image = image
            };

            return View(profileSideVM);
        }
    }
}