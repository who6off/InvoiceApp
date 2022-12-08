using System.Security.Claims;

namespace InvoiceApp.Identity.Helpers
{
    public static class ClaimsPrincipalExtentions
    {
        public static string? GetFullName(this ClaimsPrincipal user)
        {
            var fullName = user.FindFirst(c => c.Type == "FullName")?.Value;
            return fullName;
        }


        public static string? GetRole(this ClaimsPrincipal user)
        {
            var role = user.FindFirst(c => c.Type == ClaimTypes.Role)?.Value;
            return role;
        }


        public static string? GetId(this ClaimsPrincipal user)
        {
            var fullName = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            return fullName;
        }
    }
}
