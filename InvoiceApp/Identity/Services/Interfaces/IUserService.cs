using InvoiceApp.Identity.Models;
using InvoiceApp.Identity.ViewModels;
using InvoiceApp.ViewModels.User;

namespace InvoiceApp.Identity.Services.Interfaces
{
	public interface IUserService
	{
		public Task<AppUser?> SignIn(SignInViewModel model);

		public Task SignOut();

		public Task<AppUser[]> GetAll();

		public Task<AppUser?> GetById(string id);

		public Task<AppUser?> Create(UserViewModel model);

		public Task<AppUser?> Update(UserViewModel model);

		public Task<bool> Delete(string id);
	}
}
