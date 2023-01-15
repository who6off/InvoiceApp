using InvoiceApp.Identity.Models;
using InvoiceApp.Identity.ViewModels;

namespace InvoiceApp.ViewModels.User
{
	public class UserDetailsViewModel
	{
		public string Header { get; set; }

		public AppUser AppUser { get; set; }

		public UserViewModel? UserViewModel { get; set; }

		public string? ReturnUrl { get; set; }

		public UserDetailsViewModel() { }

		public UserDetailsViewModel(AppUser appUser, string header)
		{
			Header = header;
			AppUser = appUser;
		}

		public UserDetailsViewModel(AppUser appUser, UserViewModel userViewModel, string header)
		{
			Header = header;
			AppUser = appUser;
			UserViewModel = userViewModel;
		}
	}
}
