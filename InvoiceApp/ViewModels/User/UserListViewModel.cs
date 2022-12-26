using InvoiceApp.Data.RequestParameters;
using InvoiceApp.Identity.Models;

namespace InvoiceApp.ViewModels.User
{
	public class UserListViewModel
	{
		public AppUser[] Users { get; set; }

		public UserRequestParameters Parameters { get; set; } = new();
	}
}
