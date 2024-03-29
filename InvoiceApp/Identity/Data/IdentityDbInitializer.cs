﻿using InvoiceApp.Identity.Models;
using InvoiceApp.Identity.Services.Interfaces;
using InvoiceApp.Identity.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace InvoiceApp.Identity.Data
{
	public static class IdentityDbInitializer
	{
		public static async Task Initialize(IServiceProvider serviceProvider)
		{
			var options = serviceProvider.GetService<IConfiguration>()?
				.GetSection(IdentityDbInitializerOptions.SectionName)
				.Get<IdentityDbInitializerOptions>();

			if (!options.IsDataSeedingRequired)
			{
				return;
			}

			using (var scope = serviceProvider.CreateScope())
			{
				var provider = scope.ServiceProvider;

				var dbContext = provider.GetService<AppIdentityDbContext>();
				await dbContext.Database.EnsureDeletedAsync();
				await dbContext.Database.EnsureCreatedAsync();

				await SeedRoles(provider, options);

				await SeedAdmin(provider, options);
			}
		}


		private static async Task<IdentityResult[]> SeedRoles(IServiceProvider provider, IdentityDbInitializerOptions options)
		{
			var roleManager = provider.GetService<RoleManager<AppRole>>();

			var result = new IdentityResult[options.Roles.Length];
			for (int i = 0; i < options.Roles.Length; i++)
			{
				result[i] = await roleManager.CreateAsync(new AppRole(options.Roles[i]));
			}

			return result;
		}


		private static Task<AppUser?> SeedAdmin(IServiceProvider provider, IdentityDbInitializerOptions options)
		{
			var userService = provider.GetService<IUserService>();

			return userService.Create(new UserViewModel()
			{
				Name = options.MainAdmin.Name,
				Surname = options.MainAdmin.Surname,
				Email = options.MainAdmin.Email,
				DateOfBirth = options.MainAdmin.DateOfBirth,
				Password = options.MainAdmin.Password,
				Role = options.MainAdmin.Role
			});
		}
	}



	public class IdentityDbInitializerOptions
	{
		public const string SectionName = "IdentityDb";
		public bool IsDataSeedingRequired { get; set; }
		public int MinEmployeeAge { get; set; }
		public string[] Roles { get; set; }
		public MainAdminOptions MainAdmin { get; set; }

		public class MainAdminOptions
		{
			public string Name { get; set; }
			public string Surname { get; set; }
			public DateTime DateOfBirth { get; set; }
			public string Email { get; set; }
			public string Password { get; set; }
			public string Role { get; set; }
		}
	}
}
