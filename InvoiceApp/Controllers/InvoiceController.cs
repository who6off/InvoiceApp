using InvoiceApp.Data.Models;
using InvoiceApp.Helpers;
using InvoiceApp.Services.Interfaces;
using InvoiceApp.ViewModels.Invoice;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceApp.Controllers
{
    public class InvoiceController : Controller
    {
        private const string NewInvoice = "NewInvoice";

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


        public async Task<IActionResult> List()
        {
            var invoices = await _invoiceService.GetAll();
            return View(new ListViewModel()
            {
                Invoices = invoices
            });
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

            if (invoice is null)
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);

            TempData.Put<Invoice>(NewInvoice, invoice);
            return RedirectToAction(nameof(Details));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var invoice = await _invoiceService.GetById(id);

            return View(new InvoiceViewModel()
            {
                Id = invoice.Id,
                Owner = invoice.Owner.Name,
                Amount = invoice.Amount,
                Month = invoice.Month,
            });
        }


        [HttpPost]
        public async Task<IActionResult> Edit(InvoiceViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            Invoice? invoice;
            try
            {
                invoice = await _invoiceService.Update(model);
            }
            catch (ModelValidationException e)
            {
                ModelState.AddModelError(e.Propery, e.Message);
                return View(model);
            }

            if (invoice is null)
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);

            TempData.Put<Invoice>(NewInvoice, invoice);
            return RedirectToAction(nameof(Details));
        }


        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var invoice = TempData.Get<Invoice>(NewInvoice);
            return View(invoice);
        }
    }
}
