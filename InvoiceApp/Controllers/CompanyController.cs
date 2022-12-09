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
        public async Task<IActionResult> Index(string? newCompanyName)
        {
            var companies = await _companyServce.GetAll();
            return View(new IndexViewModel()
            {
                NewCompanyName = newCompanyName,
                Companies = companies
            });
        }


        [HttpPost]
        public async Task<IActionResult> Create(IndexViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Companies = await _companyServce.GetAll();
                return View("Index", viewModel);
            }

            var isCreated = await _companyServce.Create(viewModel.NewCompanyName);

            return RedirectToAction(nameof(Index));
        }

    }
}
