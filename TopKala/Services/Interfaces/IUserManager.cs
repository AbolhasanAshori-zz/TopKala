using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopKala.DataAccess.Repository.IRepository;
using TopKala.Models;

namespace TopKala.Services.Interfaces
{
    public interface IUserManager
    {
        IUserRepository _repository { get; }

        bool CheckUsername(string username);
        bool CheckEmail(string email);
        bool CheckPhoneNumber(string phoneNumber);
        bool VerifyEmail(string email);
        bool VerifyPhoneNumber(string phoneNumber);
        bool VerifyPassword(User user, string passwordHash);
        User ValidatePasswordWithUsername(string username, string passwordHash, bool IsPersistent = false);
        User ValidatePasswordWithEmail(string email, string passwordHash, bool IsPersistent = false);
        User ValidatePasswordWithPhoneNumber(string phoneNumber, string passwordHash, bool IsPersistent = false);
    }
}