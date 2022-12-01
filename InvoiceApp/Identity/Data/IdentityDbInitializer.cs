using InvoiceApp.Identity.Models;
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
				.GetSection(IdentityDbInitializerOtions.SectionName)
				.Get<IdentityDbInitializerOtions>();

			if (!options.IsDataSeedingRequired) return;

			using (var scope = serviceProvider.CreateScope())
			{
				await SeedRoles(scope, options);

				await SeedAdmin(scope, options);
			}
		}


		private static async Task<IdentityResult[]> SeedRoles(IServiceScope scope, IdentityDbInitializerOtions options)
		{
			var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

			var result = new IdentityResult[options.Roles.Length];
			for (int i = 0; i < options.Roles.Length; i++)
			{
				result[i] = await roleManager.CreateAsync(new IdentityRole(options.Roles[i]));
			}

			return result;
		}


		private static Task<AppUser?> SeedAdmin(IServiceScope scope, IdentityDbInitializerOtions options)
		{
			var userService = scope.ServiceProvider.GetService<IUserService>();

			return userService.CreateUser(new UserViewModel()
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


	public class IdentityDbInitializerOtions
	{
		public const string SectionName = "Identity";
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
