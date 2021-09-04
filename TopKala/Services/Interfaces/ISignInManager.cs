using TopKala.Models;

namespace TopKala.Services.Interfaces
{
    public interface ISignInManager
    {
        User GetUser();
        void SignIn(User user, bool isPersistent);
        void SignIn(string username, string password, bool isPersistent);
        void SignOut();
    }
}