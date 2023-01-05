﻿using InvoiceApp.Authorization;
using InvoiceApp.Data.Models;
using InvoiceApp.Data.RequestParameters;
using InvoiceApp.Helpers.Exceptions;
using InvoiceApp.Identity.Constants;
using InvoiceApp.Services.Interfaces;
using InvoiceApp.ViewModels.Invoice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceApp.Controllers
{
    public class InvoiceController : Controller
    {
        private const string NewInvoice = "NewInvoice";

        private readonly IInvoiceService _invoiceService;
        private readonly IAuthorizationService _authorizationService;

        public InvoiceController(
            IInvoiceService invoiceService,
            IAuthorizationService authorizationService)
        {
            _invoiceService = invoiceService;
            _authorizationService = authorizationService;
        }


        [Authorize]
        public IActionResult Index()
        {
            return View();
        }


        [Authorize]
        public IActionResult Create()
        {
            return View(new InvoiceViewModel());
        }


        [Authorize]
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
                ? throw new AppException("Invoice is not created.")
                : RedirectToAction(nameof(Details), new { id = invoice.Id });
        }


        [Authorize]
        public async Task<IActionResult> List([FromQuery] InvoiceRequestParemeters parameters)
        {
            var invoices = await _invoiceService.Get(parameters, User);
            return View(new ListViewModel()
            {
                ActionName = nameof(List),
                Invoices = invoices,
                Parameters = parameters
            });
        }


        [Authorize(Roles = $"{UserRoles.Manager}, {UserRoles.Admin}")]
        [Route("[controller]/List/User/{UserId:required}")]
        public async Task<IActionResult> ListByUser(InvoiceRequestParemeters parameters)
        {
            var request = this.HttpContext.Request;
            var invoices = await _invoiceService.Get(parameters);
            return View("List", new ListViewModel()
            {
                ActionName = nameof(ListByUser),
                Invoices = invoices,
                Parameters = parameters
            });
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var invoice = await _invoiceService.GetById(id);

            if (invoice is null)
            {
                throw new NotFoundException("Invoice not found.");
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(
                User, invoice, InvoiceOperations.Read);

            if (!isAuthorized.Succeeded)
            {
                throw new AccessDeniedException("You can not edit this invoice.");
            }

            return View(new InvoiceViewModel()
            {
                Id = invoice.Id,
                Owner = invoice.Owner.Name,
                Amount = invoice.Amount,
                Month = invoice.Month,
            });
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(InvoiceViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var invoice = await _invoiceService.GetById(model.Id.Value);

            if (invoice is null)
            {
                throw new NotFoundException("Invoice not found.");
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(
                User, invoice, InvoiceOperations.Update);

            if (!isAuthorized.Succeeded)
            {
                throw new AccessDeniedException("You can not edit this invoice.");
            }

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


        //[Authorize]
        //[HttpGet]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var invoice = await _invoiceService.GetById(id);
        //    var isAuthorized = await _authorizationService.AuthorizeAsync(
        //        User, invoice, InvoiceOperations.Delete);

        //    if (!isAuthorized.Succeeded)
        //    {
        //        throw new AccessDeniedException("You are not allowed to delete this invoice!");
        //    }

        //    var isDeleted = await _invoiceService.Delete(id);

        //    return isDeleted
        //        ? RedirectToAction(nameof(List))
        //        : new StatusCodeResult(StatusCodes.Status500InternalServerError);
        //}


        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var invoice = await _invoiceService.GetById(id);
            var isAuthorized = await _authorizationService.AuthorizeAsync(
                User, invoice, InvoiceOperations.Delete);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            var isDeleted = await _invoiceService.Delete(id);

            return isDeleted ? Ok() : BadRequest();
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var invoice = await _invoiceService.GetById(id);

            if (invoice is null)
            {
                throw new NotFoundException("Invoice not found.");
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(
                User, invoice, InvoiceOperations.Read);

            if (!isAuthorized.Succeeded)
            {
                throw new AccessDeniedException("You can not see this invoice.");
            }

            return View(invoice);
        }


        [Authorize(Roles = $"{UserRoles.Manager}, {UserRoles.Admin}")]
        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int id, string status)
        {
            var invoice = await _invoiceService.ChangeStatus(id, status);
            return RedirectToAction(nameof(Details), new { id = id });
        }
    }
}
