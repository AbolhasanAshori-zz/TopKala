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
                throw new UserExistException("کاربر مورد نظر با نام کاربری وارد شده در سایت موجود می باشد") : false;

        public bool CheckEmail(string email)
            => _repository.GetFirstOrDefault(u => u.Email == email) != null ? 
                throw new UserExistException("کاربر مورد نظر با ایمیل وارد شده در سایت موجود می باشد") : false;

        public bool CheckPhoneNumber(string phoneNumber)
            => _repository.GetFirstOrDefault(u => u.PhoneNumber == phoneNumber) != null ? 
                throw new UserExistException("کاربر مورد نظر با شماره موبایل وارد شده در سایت موجود می باشد") : false;

        public bool VerifyEmail(string email)
            => new EmailAddressAttribute().IsValid(email);

        public bool VerifyPhoneNumber(string phoneNumber)
            => Regex.IsMatch(phoneNumber, TopKala.Utility.Regex.NativePhoneNumber);

        public bool VerifyPassword(User user, string passwordHash)
            => BCrypt.Net.BCrypt.Verify(passwordHash, user.PasswordHash);

        public User ValidatePasswordWithEmail(string email, string passwordHash, bool IsPersistent = false)
        {
            if (!VerifyEmail(email))
                throw new UserNotFoundException();
            var user = _repository.GetFirstOrDefault(u => u.Email == email, "Role");
            return VerifyPassword(user, passwordHash) ? user : throw new UserNotFoundException();
        }

        public User ValidatePasswordWithPhoneNumber(string phoneNumber, string passwordHash, bool IsPersistent = false)
        {
            if (!VerifyPhoneNumber(phoneNumber))
                throw new UserNotFoundException();
            var user = _repository.GetFirstOrDefault(u => u.PhoneNumber == phoneNumber, "Role");
            return VerifyPassword(user, passwordHash) ? user : throw new UserNotFoundException();
        }

        public User ValidatePasswordWithUsername(string username, string passwordHash, bool IsPersistent = false)
        {
            var user = _repository.GetFirstOrDefault(u => u.Username == username, "Role");
            return VerifyPassword(user, passwordHash) ? user : throw new UserNotFoundException();
        }
    }
}