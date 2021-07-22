using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using TopKala.Models;
using TopKala.Services.Interfaces;

namespace TopKala.Services
{
    public class SignInManager : ISignInManager
    {
        private readonly HttpContext httpContext;

        public SignInManager(IHttpContextAccessor httpContextAccessor)
        {
            httpContext = httpContextAccessor.HttpContext;
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

            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties() 
            {
                IsPersistent = isPersistent
            });
        }

        public async void SignOut()
            => await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}