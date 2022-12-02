using InvoiceApp.Helpers;
using InvoiceApp.Identity.Models;
using InvoiceApp.Identity.Services.Interfaces;
using InvoiceApp.Identity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(
            UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task<AppUser[]> GetAll()
        {
            var users = await _userManager.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .ToArrayAsync();

            return users;
        }


        public async Task<AppUser?> CreateUser(UserViewModel model)
        {
            var appUser = new AppUser()
            {
                Name = model.Name.FirstCharToUpper(),
                Surname = model.Surname.FirstCharToUpper(),
                DateObBirth = model.DateOfBirth,
                Email = model.Email,
                UserName = model.Email.ToLower(),
            };

            var result = await _userManager.CreateAsync(appUser, model.Password);

            if (result.Succeeded)
            {
                var newUser = await _userManager.FindByEmailAsync(model.Email);

                var roleResult = await _userManager.AddToRoleAsync(newUser, model.Role);
                return newUser;
            }

            return null;
        }
    }
}
