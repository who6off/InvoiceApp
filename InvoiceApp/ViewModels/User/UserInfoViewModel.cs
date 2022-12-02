using InvoiceApp.Identity.Models;
using InvoiceApp.Identity.ViewModels;

namespace InvoiceApp.ViewModels.User
{
    public class UserInfoViewModel
    {
        public AppUser AppUser { get; set; }

        public UserViewModel UserViewModel { get; set; }

        public UserInfoViewModel() { }
        public UserInfoViewModel(AppUser appUser, UserViewModel userViewModel)
        {
            AppUser = appUser;
            UserViewModel = userViewModel;
        }
    }
}
