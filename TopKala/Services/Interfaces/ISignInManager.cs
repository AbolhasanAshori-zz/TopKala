using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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