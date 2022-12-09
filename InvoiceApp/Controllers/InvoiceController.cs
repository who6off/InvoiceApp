using Microsoft.AspNetCore.Mvc;

namespace InvoiceApp.Controllers
{
	public class InvoiceController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}


	}
}
