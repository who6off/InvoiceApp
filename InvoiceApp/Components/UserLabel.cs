using InvoiceApp.Identity.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceApp.Components
{
	public class UserLabel : ViewComponent
	{
		private readonly HttpContext _httpContext;

		public UserLabel(
			IHttpContextAccessor httpContextAccessor
			)
		{
			_httpContext = httpContextAccessor.HttpContext;
		}


		public IViewComponentResult Invoke()
		{
			var fullName = this.UserClaimsPrincipal.GetFullName();
			return View((object)fullName);
		}
	}
}
