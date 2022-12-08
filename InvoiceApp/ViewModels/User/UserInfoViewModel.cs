using InvoiceApp.Identity.Models;
using InvoiceApp.Identity.ViewModels;

namespace InvoiceApp.ViewModels.User
{
	public class UserInfoViewModel
	{
		public string Header { get; set; }

		public AppUser AppUser { get; set; }

		public UserViewModel UserViewModel { get; set; }

		public UserInfoViewModel() { }
		public UserInfoViewModel(AppUser appUser, UserViewModel userViewModel, string header)
		{
			Header = header;
			AppUser = appUser;
			UserViewModel = userViewModel;
		}
	}
}
