using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
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
            return View();
        }

        public IActionResult Orders()
        {
            // TODO: Create Product Orders
            return View();
        }

        public IActionResult OrdersReturn()
        {
            // TODO: Create Order Return
            return View();
        }

        public IActionResult Favorites()
        {
            // TODO: Create User Favorite Product List
            return View();
        }

        public IActionResult Info()
        {
            return View();
        }

        [Route("[controller]/Edit/" + nameof(Info))]
        public IActionResult InfoEdit()
        {
            var user = _signInManager.GetUser();
            ProfileInfoEditVM profileInfoEditVM = new ProfileInfoEditVM()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                IdNumber = user.IdNumber,
                CardNumber = user.CardNumber,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Subscript = user.IsNewsletterSubscripted,
                ForeignUser = user.IsUserForeign
            };
            return View(profileInfoEditVM);
        }

        [HttpPost("[controller]/Edit/" + nameof(Info))]
        [ValidateAntiForgeryToken]
        public IActionResult InfoEdit(ProfileInfoEditVM profileInfoEditVM)
        {
            if (!ModelState.IsValid)
            {
                var user = _signInManager.GetUser();

                profileInfoEditVM.FirstName = user.FirstName;
                profileInfoEditVM.LastName = user.LastName;
                profileInfoEditVM.IdNumber = user.IdNumber;
                profileInfoEditVM.CardNumber = user.CardNumber;
                profileInfoEditVM.PhoneNumber = user.PhoneNumber;
                profileInfoEditVM.Email = user.Email;
                profileInfoEditVM.Subscript = user.IsNewsletterSubscripted;
                profileInfoEditVM.ForeignUser = user.IsUserForeign;
                return View(profileInfoEditVM);
            }

            var userEdited = _signInManager.GetUser();
            userEdited.FirstName = profileInfoEditVM.FirstName;
            userEdited.LastName = profileInfoEditVM.LastName;
            userEdited.CardNumber = profileInfoEditVM.CardNumber;
            userEdited.IsNewsletterSubscripted = profileInfoEditVM.Subscript;
            userEdited.IsUserForeign = profileInfoEditVM.ForeignUser;

            if (profileInfoEditVM.ForeignUser)
            {
                userEdited.IdNumber = null;
            }
            else
            {
                userEdited.IdNumber = profileInfoEditVM.IdNumber;
            }

            if (userEdited.PhoneNumber != profileInfoEditVM.PhoneNumber)
            {
                userEdited.PhoneNumber = profileInfoEditVM.PhoneNumber;
                userEdited.IsPhoneNumberConfirmed = false;
            }

            if (userEdited.Email.ToLower() != profileInfoEditVM.Email.ToLower())
            {
                userEdited.Email = profileInfoEditVM.Email.ToLower();
                userEdited.IsEmailConfirmed = false;
            }

            _unitOfWork.Save();
            
            return View(nameof(Info));
        }

        [Route("[controller]/Change/[action]")]
        public IActionResult Password()
        {
            return View();
        }

        [HttpPost("[controller]/Change/[action]")]
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

            var user = _signInManager.GetUser();

            if (!BCrypt.Net.BCrypt.Verify(passwordVM.OldPassword, user.PasswordHash))
            {
                ViewBag.Message = "رمز عبور قبلی نادرست می باشد";
                return View();
            }
                
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(passwordVM.NewPassword);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        
        [Route("[controller]/Change/[action]")]
        public IActionResult Username(string ReturnUrl = "/")
        {
            UsernameVM usernameVM = new UsernameVM()
            {
                OldUsername = _signInManager.GetUser().Username,
                ReturnUrl = ReturnUrl
            };
            return View(usernameVM);
        }

        [HttpPost("[controller]/Change/[action]")]
        [ValidateAntiForgeryToken]
        public IActionResult Username(UsernameVM usernameVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (usernameVM.OldUsername == usernameVM.NewUsername)
            {
                ViewBag.Message = "نام کاربری نمی تواند تکراری باشد";
                return View();
            }

            if (_unitOfWork.User.GetFirstOrDefault(u => u.Username == usernameVM.NewUsername) != null)
            {
                ViewBag.Message = "نام کاربری از قبل وجود دارد";
                return View(); 
            }

            var user = _signInManager.GetUser();
            user.Username = usernameVM.NewUsername;
            _unitOfWork.Save();
            _signInManager.SignOut();

            user = _unitOfWork.User.GetFirstOrDefault(u => u.Username == usernameVM.NewUsername, "Role");
            _signInManager.SignIn(user, false);
            return LocalRedirect(usernameVM.ReturnUrl);
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