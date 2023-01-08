using InvoiceApp.Data.Models;
using InvoiceApp.Data.RequestParameters;
using InvoiceApp.Helpers;
using InvoiceApp.ViewModels.Company;

namespace InvoiceApp.Services.Interfaces
{
	public interface ICompanyService
	{
		public Task<Company?> GetById(int id);

		public Task<Company?> GetByName(string name);

		public Task<List<Company>> GetAll();

		public Task<PagedList<Company>> Get(CompanyRequestParameters parameters);

		public Task<Company?> Create(CompanyViewModel viewModel);

		public Task<Company?> Update(CompanyViewModel viewModel);

		public Task<bool> Delete(int id);
	}
}
