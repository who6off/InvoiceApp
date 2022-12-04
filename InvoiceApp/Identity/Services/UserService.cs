using InvoiceApp.Helpers;
using InvoiceApp.Identity.Models;
using InvoiceApp.Identity.Services.Interfaces;
using InvoiceApp.Identity.ViewModels;
using InvoiceApp.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.Identity.Services
{
	public class UserService : IUserService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IPasswordHasher<AppUser> _passwordHasher;
		private readonly SignInManager<AppUser> _signInManager;

		public UserService(
			UserManager<AppUser> userManager,
			IPasswordHasher<AppUser> passwordHasher,
			SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_passwordHasher = passwordHasher;
			_signInManager = signInManager;
		}


		public async Task<AppUser?> SignIn(SignInViewModel model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user is null) return null;

			await _signInManager.SignOutAsync();
			var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

			return result.Succeeded ? user : null;
		}


		public Task SignOut() => _signInManager.SignOutAsync();


		public async Task<AppUser[]> GetAll()
		{
			var users = await _userManager.Users
				.Include(u => u.UserRoles)
				.ThenInclude(ur => ur.Role)
				.ToArrayAsync();

			return users;
		}


		public async Task<AppUser?> GetById(string id)
		{
			var user = await _userManager.Users
				.Where(u => u.Id == id)
				.Include(u => u.UserRoles)
				.ThenInclude(ur => ur.Role)
				.FirstOrDefaultAsync();

			return user;
		}

		public async Task<AppUser?> Create(UserViewModel model)
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


		public async Task<AppUser?> Update(UserViewModel model)
		{
			var user = await this.GetById(model.Id);
			if (user is null) return null;

			user.Name = model.Name.FirstCharToUpper();
			user.Surname = model.Surname.FirstCharToUpper();
			user.DateObBirth = model.DateOfBirth;
			user.Email = model.Email;
			user.UserName = model.Email.ToLower();

			if (!string.IsNullOrEmpty(model.Password))
				user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);

			if (!await _userManager.IsInRoleAsync(user, model.Role))
			{
				await _userManager.RemoveFromRoleAsync(user, user.RoleName);
				await _userManager.AddToRoleAsync(user, model.Role);
			}

			var udpateResult = await _userManager.UpdateAsync(user);

			return await this.GetById(model.Id);
		}


		public async Task<bool> Delete(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user is null) return false;

			var result = await _userManager.DeleteAsync(user);

			return result.Succeeded;
		}
	}
}
