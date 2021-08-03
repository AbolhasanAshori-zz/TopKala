using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using TopKala.DataAccess.Repository.IRepository;
using TopKala.Models;
using TopKala.Services.Interfaces;

namespace TopKala.Services
{
    public class SignInManager : ISignInManager
    {
        private readonly HttpContext _context;
        private readonly IUserManager _userManager;
        private readonly IUserRepository _userRepository;

        public SignInManager(IHttpContextAccessor httpContextAccessor, IUserManager userManager, IUserRepository userRepository)
        {
            _context = httpContextAccessor.HttpContext;
            _userManager = userManager;
            _userRepository = userRepository;

        }

        public User GetUser()
        {
            // TODO: Check User Athentication
            var username = _context.User.FindFirstValue(ClaimTypes.Name);
            return _userRepository.GetFirstOrDefault(u => u.Username == username);
        }

        public async void SignIn(User user, bool isPersistent)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.Id)),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await _context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties() 
            {
                IsPersistent = isPersistent
            });
        }

        public void SignIn(string username, string password, bool isPersistent)
        {
            var user = _userManager.ValidatePasswordWithUsername(username, password);
            SignIn(user, isPersistent);
        }

        public async void SignOut()
            => await _context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}