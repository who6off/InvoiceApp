using InvoiceApp.Data.RequestParameters;
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


        //[HttpPost]
        //public async Task<IActionResult> Create(IndexViewModel viewModel)
        //{
        //    if (!ModelState.IsValid)
        //        return await GetCreateCompanyErrorResult(viewModel);

        //    try
        //    {
        //        var company = await _companyServce.Create(viewModel.NewCompanyName);
        //    }
        //    catch (ModelValidationException e)
        //    {
        //        ModelState.AddModelError(nameof(viewModel.NewCompanyName), e.Message);
        //        return await GetCreateCompanyErrorResult(viewModel);
        //    }

        //    return RedirectToAction(nameof(Index));
        //}


        //[HttpGet]
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var company = await _companyServce.GetById(id);
        //    if (company is null)
        //    {
        //        throw new NotFoundException("Company is not found.");
        //    }

        //    return View(company);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(Company company)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(company);
        //    }

        //    var updatedCompany = await _companyServce.Update(company);

        //    return RedirectToAction(nameof(Index));
        //}


        //[HttpGet]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var isDeleted = await _companyServce.Delete(id);

        //    return RedirectToAction(nameof(Index));
        //}


        //[HttpGet]
        //public async Task<IActionResult> Info(int id)
        //{
        //    var company = await _companyServce.GetById(id);
        //    if (company is null)
        //    {
        //        throw new NotFoundException("Company is not found.");
        //    }

        //    return View(new CompanyInfoViewModel()
        //    {
        //        Company = company
        //    });
        //}


        //private async Task<IActionResult> GetCreateCompanyErrorResult(IndexViewModel viewModel)
        //{
        //    viewModel.Companies = await _companyServce.GetAll();
        //    return View("Index", viewModel);
        //}
    }
}
