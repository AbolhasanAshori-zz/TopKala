using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TopKala.DataAccess.Data;
using TopKala.Models;
using TopKala.Utility.StaticData;

namespace TopKala.DataAccess.Initializer
{
    public class IdentityDbInitializer
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            await AddUserRolesAsync(context);
            await AddUsersAsync(context);
            
            await context.SaveChangesAsync();
        }

        private static async Task AddUserRolesAsync(ApplicationDbContext context)
        {
            var userRolesDbSet = context.Set<UserRole>();
            if (userRolesDbSet.Any())
            {
                return;
            }

            var userRoles = new List<UserRole>();
            
            userRoles.Add(new Models.UserRole{ Name = SD_Role.Developer});
            userRoles.Add(new Models.UserRole{ Name = SD_Role.Super_Admin});
            userRoles.Add(new Models.UserRole{ Name = SD_Role.Admin});
            userRoles.Add(new Models.UserRole{ Name = SD_Role.Moderator});
            userRoles.Add(new Models.UserRole{ Name = SD_Role.Customer});

            await userRolesDbSet.AddRangeAsync(userRoles);
        }

        private static async Task AddUsersAsync(ApplicationDbContext context)
        {
            var userDbSet = context.Set<User>();
            if (userDbSet.Any())
            {
                return;
            }

            var users = new List<User>();
            
            users.Add(new Models.User
            {
                Username = "Admin",
                //* Password is Admin123*
                PasswordHash = "$2a$11$R5/rmr1hr/5ySY6tw.VXteSyDrQ4zbbkBTM5StRL9WWqf2fz1pgE6",
                Role = context.ChangeTracker.Entries<UserRole>().FirstOrDefault(e => e.State == EntityState.Added && e.Entity.Name == SD_Role.Admin).Entity,
                Image = "user.svg",
                Email = "admin@gmail.com",
                FirstName = "Mr.",
                LastName = "Admin",
                PhoneNumber = "09123456789",
                IdNumber = "1234567812345678",
                CardNumber = "12345678",
                IsActive = true,
                IsEmailConfirmed = true,
                IsPhoneNumberConfirmed = true,
            });

            await userDbSet.AddRangeAsync(users);
        }
    }
}