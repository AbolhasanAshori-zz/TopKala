using TopKala.DataAccess.Repository.IRepository;
using TopKala.Models;

namespace TopKala.Services.Interfaces
{
    public interface IUserManager
    {
        bool CheckUsername(string username);
        bool CheckEmail(string email);
        bool CheckPhoneNumber(string phoneNumber);
        bool VerifyEmail(string email);
        bool VerifyPhoneNumber(string phoneNumber);
        bool VerifyPassword(string PasswordHash, string password);
        User ValidatePasswordWithUsername(string username, string password, bool IsPersistent = false);
        User ValidatePasswordWithEmail(string email, string password, bool IsPersistent = false);
        User ValidatePasswordWithPhoneNumber(string phoneNumber, string password, bool IsPersistent = false);
        void Create(User user, string password);
        User GetDefaultUser();
    }
}