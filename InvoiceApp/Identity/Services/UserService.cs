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
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task<AppUser[]> GetAll()
        {
            return await _userManager.Users.ToArrayAsync();
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

                if (!(await _roleManager.RoleExistsAsync(model.Role)))
                {
                    await _roleManager.CreateAsync(new IdentityRole(model.Role));
                }

                var roleResult = await _userManager.AddToRoleAsync(newUser, model.Role);
                return newUser;
            }

            return null;
        }
    }
}
