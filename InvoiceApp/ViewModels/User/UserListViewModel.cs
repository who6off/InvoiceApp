using InvoiceApp.Helpers;
using InvoiceApp.Identity.Models;
using InvoiceApp.Identity.RequestParameters;

namespace InvoiceApp.ViewModels.User
{
    public class UserListViewModel
    {
        public PagedList<AppUser> Users { get; set; }

        public UserRequestParameters Parameters { get; set; } = new();
    }
}
