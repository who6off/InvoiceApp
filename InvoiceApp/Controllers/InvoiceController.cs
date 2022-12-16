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

            return (invoice is null)
                ? new StatusCodeResult(StatusCodes.Status500InternalServerError)
                : RedirectToAction(nameof(Details), new { id = invoice.Id });
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

            return (invoice is null)
                ? new StatusCodeResult(StatusCodes.Status500InternalServerError)
                : RedirectToAction(nameof(Details), new { id = invoice.Id });
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _invoiceService.Delete(id);

            return isDeleted
                ? RedirectToAction(nameof(List))
                : new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var invoice = await _invoiceService.GetById(id);
            return View(invoice);
        }


        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int id, string status)
        {
            var invoice = await _invoiceService.ChangeStatus(id, status);
            return RedirectToAction(nameof(Details), new { id = id });
        }
    }
}
