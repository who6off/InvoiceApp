using InvoiceApp.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace InvoiceApp.Identity.Helpers
{
    public class AppClaimsFactory : UserClaimsPrincipalFactory<AppUser>
    {
        UserManager<AppUser> _userManager;
        public AppClaimsFactory(
            UserManager<AppUser> userManager,
            IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        { }

        public async override Task<ClaimsPrincipal> CreateAsync(AppUser user)
        {
            var principal = await base.CreateAsync(user);

            var role = (await UserManager.GetRolesAsync(user)).FirstOrDefault();
            ((ClaimsIdentity)principal.Identity).AddClaims(new[]
            {
                new Claim("FullName", $"{user.Name} {user.Surname}"),
                new Claim(ClaimTypes.Role, role)
            });


            return principal;
        }
    }
}
