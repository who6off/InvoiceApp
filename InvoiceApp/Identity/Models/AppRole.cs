using Microsoft.AspNetCore.Identity;

namespace InvoiceApp.Identity.Models
{
    public class AppRole : IdentityRole
    {
        public AppRole(string name) : base(name) { }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }

    public class UserRole : IdentityUserRole<string>
    {
        public virtual AppUser User { get; set; }
        public virtual AppRole Role { get; set; }
    }
}
