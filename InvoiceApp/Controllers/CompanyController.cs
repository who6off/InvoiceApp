using InvoiceApp.Data.Models;
using InvoiceApp.Data.RequestParameters;
using InvoiceApp.Helpers.Exceptions;
using InvoiceApp.Services.Interfaces;
using InvoiceApp.ViewModels.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceApp.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyServce;


        public CompanyController(ICompanyService companyService)
        {
            _companyServce = companyService;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index([FromQuery] CompanyRequestParameters parameters)
        {
            var companies = await _companyServce.Get(parameters);
            return View(new IndexViewModel()
            {
                Parameters = parameters,
                Companies = companies
            });
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            return View(new CompanyViewModel());
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CompanyViewModel viewModel, string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            Company? company;

            try
            {
                company = await _companyServce.Create(viewModel);
            }
            catch (ModelValidationException e)
            {
                ModelState.AddModelError(e.Propery, e.Message);
                return View(viewModel);
            }

            if (company is null)
            {
                throw new AppException("Company is not created!");
            }

            return RedirectToAction(nameof(Details), new { id = company.Id, returnUrl = returnUrl });
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var company = await _companyServce.GetById(id);
            if (company is null)
            {
                throw new NotFoundException("Company is not found.");
            }

            return View(new CompanyViewModel()
            {
                Id = company.Id,
                Name = company.Name,
            });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(CompanyViewModel viewModel, string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            Company? updatedCompany;

            try
            {
                updatedCompany = await _companyServce.Update(viewModel);
            }
            catch (ModelValidationException e)
            {
                ModelState.AddModelError(e.Propery, e.Message);
                return View(viewModel);
            }

            if (updatedCompany is null)
            {
                throw new AppException("Company is not updated!");
            }

            return string.IsNullOrEmpty(returnUrl)
                ? RedirectToAction(nameof(Details), new { id = updatedCompany.Id })
                : Redirect(returnUrl);
        }


        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _companyServce.Delete(id);

            return isDeleted ? Ok() : BadRequest();
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var company = await _companyServce.GetById(id);
            if (company is null)
            {
                throw new NotFoundException("Company is not found.");
            }

            return View(new CompanyInfoViewModel()
            {
                Company = company
            });
        }
    }
}
