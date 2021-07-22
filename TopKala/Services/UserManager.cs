using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BCrypt.Net;
using TopKala.DataAccess.Repository.IRepository;
using TopKala.Exceptions;
using TopKala.Models;
using TopKala.Services.Interfaces;

namespace TopKala.Services
{
    public class UserManager : IUserManager
    {
        public IUserRepository _repository { get; set; }
        public UserManager(IUserRepository repository)
        {
            _repository = repository;
        }

        public bool CheckUsername(string username)
            => _repository.GetFirstOrDefault(u => u.Username == username) != null ? 
                throw new UserExistException("کاربر مورد نظر با نام کاربری وارد شده در سایت موجود می باشد") : true;

        public bool CheckEmail(string email)
            => _repository.GetFirstOrDefault(u => u.Email.ToLower() == email.ToLower()) != null ? 
                throw new UserExistException("کاربر مورد نظر با ایمیل وارد شده در سایت موجود می باشد") : true;

        public bool CheckPhoneNumber(string phoneNumber)
            => _repository.GetFirstOrDefault(u => u.PhoneNumber == phoneNumber) != null ? 
                throw new UserExistException("کاربر مورد نظر با شماره موبایل وارد شده در سایت موجود می باشد") : true;

        public bool VerifyEmail(string email)
            => new EmailAddressAttribute().IsValid(email);

        public bool VerifyPhoneNumber(string phoneNumber)
            => Regex.IsMatch(phoneNumber, TopKala.Utility.Regex.NativePhoneNumber);

        public bool VerifyPassword(User user, string password)
            => BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

        public User ValidatePasswordWithEmail(string email, string password, bool IsPersistent = false)
        {
            if (!VerifyEmail(email))
                throw new UserNotFoundException();
            var user = _repository.GetFirstOrDefault(u => u.Email == email, "Role");
            return VerifyPassword(user, password) ? user : throw new UserNotFoundException();
        }

        public User ValidatePasswordWithPhoneNumber(string phoneNumber, string password, bool IsPersistent = false)
        {
            if (!VerifyPhoneNumber(phoneNumber))
                throw new UserNotFoundException();
            var user = _repository.GetFirstOrDefault(u => u.PhoneNumber == phoneNumber, "Role");
            return VerifyPassword(user, password) ? user : throw new UserNotFoundException();
        }

        public User ValidatePasswordWithUsername(string username, string password, bool IsPersistent = false)
        {
            var user = _repository.GetFirstOrDefault(u => u.Username == username, "Role");
            return VerifyPassword(user, password) ? user : throw new UserNotFoundException();
        }
    }
}