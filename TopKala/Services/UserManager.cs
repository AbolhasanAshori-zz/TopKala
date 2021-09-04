using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using TopKala.DataAccess.Repository.IRepository;
using TopKala.Enums;
using TopKala.Exceptions;
using TopKala.Models;
using TopKala.Services.Interfaces;
using TopKala.Utility.StaticData;

namespace TopKala.Services
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IFileService _fileService;

        public UserManager(IUserRepository repository, IUnitOfWork unitOfWork, IFileService fileService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        public bool CheckUsername(string username)
        {   
            var user = _repository.GetFirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                throw new UserExistException("کاربر مورد نظر با نام کاربری وارد شده در سایت موجود می باشد");
            }
            return true;
        }

        public bool CheckEmail(string email)
        {
            var user = _repository.GetFirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                throw new UserExistException("کاربر مورد نظر با ایمیل وارد شده در سایت موجود می باشد");
            }
            return true;
        }

        public bool CheckPhoneNumber(string phoneNumber)
        {
            var user = _repository.GetFirstOrDefault(u => u.PhoneNumber.ToLower() == phoneNumber.ToLower());
            if (user != null)
            {
                throw new UserExistException("کاربر مورد نظر با شماره موبایل وارد شده در سایت موجود می باشد");
            }
            return true;
        }

        public bool VerifyEmail(string email)
            => new EmailAddressAttribute().IsValid(email);

        public bool VerifyPhoneNumber(string phoneNumber)
            => Regex.IsMatch(phoneNumber, SD_Regex.NativePhoneNumber);

        public bool VerifyPassword(string passwordHash, string password)
            => BCrypt.Net.BCrypt.Verify(password, passwordHash);

        public User ValidatePasswordWithEmail(string email, string password, bool IsPersistent = false)
        {
            var user = _repository.GetFirstOrDefault(u => u.Email == email, "Role");
            if (user == null || !VerifyPassword(user?.PasswordHash, password))
            {
                throw new UserNotFoundException("کاربر مورد نظر یافت نشد");
            }
            return user;
        }

        public User ValidatePasswordWithPhoneNumber(string phoneNumber, string password, bool IsPersistent = false)
        {
            var user = _repository.GetFirstOrDefault(u => u.PhoneNumber == phoneNumber, "Role");
            if (user == null || !VerifyPassword(user?.PasswordHash, password))
            {
                throw new UserNotFoundException("کاربر مورد نظر یافت نشد");
            }
            return user;
        }

        public User ValidatePasswordWithUsername(string username, string password, bool IsPersistent = false)
        {
            var user = _repository.GetFirstOrDefault(u => u.Username == username, "Role");
            if (user == null || !VerifyPassword(user.PasswordHash, password))
            {
                throw new UserNotFoundException("کاربر مورد نظر یافت نشد");
            }
            return user;
        }

        public void Create(User user, string passowrd)
        {
            var newUser = GetDefaultUser();
            newUser.Username = user.Username;
            newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(passowrd);
            newUser.Email ??= user.Email;
            newUser.PhoneNumber ??= user.PhoneNumber;

            _unitOfWork.User.Add(newUser);
            _unitOfWork.Save();
        }

        public User GetDefaultUser()
        {
            User user = new User();
            user.Image = "user.svg";
            user.IsImageUploaded = false;
            user.IsEmailConfirmed = false;
            user.IsActive = true;
            user.Role = _unitOfWork.UserRole.GetFirstOrDefault(r => r.Name == SD_Role.Customer);
            
            return user;
        }
    }
}