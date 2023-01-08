using InvoiceApp.Data.Models;
using InvoiceApp.Data.RequestParameters;
using InvoiceApp.Helpers.Exceptions;
using InvoiceApp.Services.Interfaces;
using InvoiceApp.ViewModels.Company;
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
		public async Task<IActionResult> Create()
		{
			return View(new CompanyViewModel());
		}


		[HttpPost]
		public async Task<IActionResult> Create(CompanyViewModel viewModel)
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

			return RedirectToAction(nameof(Info), new { id = company.Id });
		}


		[HttpGet]
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
		public async Task<IActionResult> Edit(CompanyViewModel viewModel)
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

			return RedirectToAction(nameof(Info), new { id = updatedCompany.Id });
		}


		//[HttpGet]
		//public async Task<IActionResult> Delete(int id)
		//{
		//    var isDeleted = await _companyServce.Delete(id);

		//    return RedirectToAction(nameof(Index));
		//}


		[HttpGet]
		public async Task<IActionResult> Info(int id)
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
