using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceApp.Identity.Models
{
	public class AppUser : IdentityUser
	{
		[Required]
		[StringLength(50, MinimumLength = 2)]
		public string Name { get; set; }

		[Required]
		[StringLength(100, MinimumLength = 2)]
		public string Surname { get; set; }

		[Required]
		[Column(TypeName = "date")]
		public DateTime DateObBirth { get; set; }

		public string? RoleName { get => UserRoles.FirstOrDefault()?.Role.Name; }

		public virtual ICollection<UserRole> UserRoles { get; set; }
	}
}
