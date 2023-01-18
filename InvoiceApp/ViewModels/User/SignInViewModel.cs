using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.ViewModels.User
{
	public class SignInViewModel
	{

		[Required(ErrorMessage = "Input the email")]
		[EmailAddress(ErrorMessage = "Incorrect email format")]
		public string Email { get; set; }


		[Required(ErrorMessage = "Input the password")]
		public string Password { get; set; }
	}
}
