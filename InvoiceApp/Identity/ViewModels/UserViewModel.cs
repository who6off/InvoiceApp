using InvoiceApp.Helpers;
using InvoiceApp.Identity.Constants;
using InvoiceApp.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.Identity.ViewModels
{
	public class UserViewModel : IValidatableObject
	{
		[Required(ErrorMessage = "Input the name")]
		[StringLength(50, MinimumLength = 2, ErrorMessage = "The name must contain more than 2 and less than 50 symbols")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Input the surname")]
		[StringLength(100, MinimumLength = 2, ErrorMessage = "The surname must contain more than 2 and less than 100 symbols")]
		public string Surname { get; set; }

		[Required(ErrorMessage = "Input the email")]
		[EmailAddress(ErrorMessage = "Incorrect email format")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Input the date of birth")]
		public DateTime DateOfBirth { get; set; } = DateTime.Now;

		[Required(ErrorMessage = "Input the password")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Repeat your password")]
		[Compare("Password", ErrorMessage = "Passwords must be equal")]
		public string PasswordRepeat { get; set; }

		[Required(ErrorMessage = "Choose the role")]
		public string Role { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var errors = new List<ValidationResult>();
			var minAge = validationContext.GetService<IConfiguration>().GetMinEmployeeAge();
			var userManager = validationContext.GetService<UserManager<AppUser>>();
			var validators = userManager?.PasswordValidators;

			if (validators is not null)
			{
				foreach (var validator in validators)
				{
					var result = validator.ValidateAsync(userManager, null, Password).GetAwaiter().GetResult();
					if (!result.Succeeded)
					{
						foreach (var error in result.Errors)
						{
							errors.Add(new ValidationResult(error.Description, new string[] { "Password" }));
						}
					}
				}
			}

			if (!UserRoles.GetAll().Contains(Role))
			{
				errors.Add(new ValidationResult("Incorrect role", new string[] { "Role" }));
			}


			var diff = DateTime.Now.GetYearDifference(DateOfBirth);
			if (diff < minAge)
			{
				errors.Add(new ValidationResult($"User must be older than {minAge}", new string[] { "DateOfBirth" }));
			}

			return errors;
		}
	}
}
