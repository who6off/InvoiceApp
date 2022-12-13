using InvoiceApp.Data.Models;
using InvoiceApp.Helpers;
using InvoiceApp.Services.Interfaces;
using InvoiceApp.ViewModels.Invoice;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceApp.Controllers
{
	public class InvoiceController : Controller
	{
		private readonly IInvoiceService _invoiceService;

		public InvoiceController(IInvoiceService invoiceService)
		{
			_invoiceService = invoiceService;
		}
		public IActionResult Index()
		{
			return View();
		}


		public IActionResult Create()
		{
			return View(new InvoiceViewModel());
		}


		[HttpPost]
		public async Task<IActionResult> Create(InvoiceViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			Invoice? invoice;

			try
			{
				invoice = await _invoiceService.Create(model);
			}
			catch (ModelValidationException e)
			{
				ModelState.AddModelError(e.Propery, e.Message);
				return View(model);
			}

			return RedirectToAction("Index");
		}
	}
}
