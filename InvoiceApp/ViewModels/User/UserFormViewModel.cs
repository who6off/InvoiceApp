using InvoiceApp.Identity.ViewModels;

namespace InvoiceApp.ViewModels.User
{
	public class UserFormViewModel
	{
		public UserViewModel User { get; set; }
		public string? ControllerName { get; set; }
		public string? ActionName { get; set; }
		public string? SubmitButtonValue { get; set; }
	}
}
