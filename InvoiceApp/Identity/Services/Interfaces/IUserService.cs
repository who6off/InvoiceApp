using InvoiceApp.Identity.Models;
using InvoiceApp.Identity.ViewModels;

namespace InvoiceApp.Identity.Services.Interfaces
{
	public interface IUserService
	{
		public Task<AppUser[]> GetAll();

		public Task<AppUser?> GetById(string id);

		public Task<AppUser?> Create(UserViewModel model);

		public Task<AppUser?> Update(UserViewModel model);
	}
}
