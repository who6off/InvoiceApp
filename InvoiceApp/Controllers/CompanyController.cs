using Microsoft.AspNetCore.Mvc;

namespace InvoiceApp.Controllers
{
	public class CompanyController : Controller
	{
		public CompanyController()
		{

		}
		public IActionResult Index()
		{
			return View();
		}
	}
}
