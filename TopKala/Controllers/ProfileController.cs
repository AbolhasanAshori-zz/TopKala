using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TopKala.DataAccess.Repository.IRepository;
using TopKala.Enums;
using TopKala.Models.ViewModels;
using TopKala.Services.Interfaces;

namespace TopKala.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISignInManager _signInManager;
        private readonly IFileService _fileService;
        private readonly ILogger<ProfileController> _logger;
        public ProfileController(IUnitOfWork unitOfWork, ISignInManager signInManager, IFileService fileService, ILogger<ProfileController> logger)
        {
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
            _fileService = fileService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var user = _signInManager.GetUser();
            string image;
            var fullname = (!string.IsNullOrEmpty(user.FirstName) ? user.FirstName : "") 
                + " "
                + (!string.IsNullOrEmpty(user.LastName) ? user.LastName : "");
            var profileName = string.IsNullOrWhiteSpace(fullname) ? user.Username : fullname;

            if (user.IsImageUploaded)
                image = _fileService.GetPath(user.Image, UploadType.Profile);
            else
                image = _fileService.GetPath(user.Image, FileType.Profile);
            
            ProfileVM profileVM = new ProfileVM()
            {
                User = user,
                ProfileSideVM = new ProfileSideVM()
                {
                    Image = image,
                    ProfileName = profileName
                }
            };
            return View(profileVM);
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

        #region Api Calls
        [HttpPut]
        public IActionResult ChangeProfile([FromBody] string data)
        {
            string imageName;
            bool isImageUploaded;
            IActionResult response;

            if (Uri.IsWellFormedUriString(data, UriKind.RelativeOrAbsolute))
            {
                imageName = data.Split('/').Last();
                isImageUploaded = false;
                response = Ok();
            }
            else {
                var dataParts = data.Split(';');

                var mime = dataParts[0].Replace("data:", "");
                var ext = MimeTypes.MimeTypeMap.GetExtension(mime);

                var imageEncoded = dataParts[1].Replace("base64,", "");
                var image = Convert.FromBase64String(imageEncoded);

                var imagePath = _fileService.UploadCreate(image, ext, UploadType.Profile);
                imageName = imagePath.Split('/').Last();
                isImageUploaded = true;

                response = Created(imagePath, null);
            }

            var user = _signInManager.GetUser();

            if (user.IsImageUploaded)
            {
                var oldImage = user.Image;
                if (!_fileService.Delete(oldImage, UploadType.Profile))
                {
                    if (isImageUploaded)
                        _fileService.Delete(imageName, UploadType.Profile);
                    return BadRequest();
                }
            }

            user.Image = imageName;
            user.IsImageUploaded = isImageUploaded;
            _unitOfWork.Save();

            return response;
        }
        #endregion
    }
}