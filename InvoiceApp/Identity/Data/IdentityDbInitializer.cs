using InvoiceApp.Helpers;
using InvoiceApp.Identity.Constants;
using Microsoft.AspNetCore.Identity;

namespace InvoiceApp.Identity.Data
{
	public static class IdentityDbInitializer
	{
		public static async Task Initialize()
		{
			await IdentityDbInitializer.InitRoles();
		}

		private static async Task InitRoles()
		{
			var roleManager = ServiceAccessor.Get<RoleManager<IdentityRole>>();
			foreach (var item in UserRoles.GetAll())
			{
				await roleManager.CreateAsync(new IdentityRole(item));
			}
		}
	}
}
