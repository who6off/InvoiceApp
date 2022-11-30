using InvoiceApp.Identity.Models;
using InvoiceApp.Identity.ViewModels;

namespace InvoiceApp.Identity.Services.Interfaces
{
	public interface IUserService
	{
		public Task<AppUser?> CreateUser(UserViewModel model);
	}
}
