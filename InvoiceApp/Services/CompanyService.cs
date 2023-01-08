using InvoiceApp.Data.Models;
using InvoiceApp.Data.Repositories.Interfaces;
using InvoiceApp.Data.RequestParameters;
using InvoiceApp.Helpers;
using InvoiceApp.Helpers.Exceptions;
using InvoiceApp.Services.Interfaces;
using InvoiceApp.ViewModels.Company;

namespace InvoiceApp.Services
{
	public class CompanyService : ICompanyService
	{
		private readonly ICompanyRepository _repository;


		public CompanyService(ICompanyRepository repository)
		{
			_repository = repository;
		}


		public Task<Company?> GetById(int id)
		{
			return _repository.GetById(id);
		}


		public Task<Company?> GetByName(string name)
		{
			return _repository.GetByName(name);
		}


		public Task<List<Company>> GetAll()
		{
			return _repository.GetAll();
		}


		public Task<PagedList<Company>> Get(CompanyRequestParameters parameters)
		{
			return _repository.Get(parameters);
		}


		public async Task<Company?> Create(CompanyViewModel viewModel)
		{
			await ValidateName(viewModel.Name);

			var newCompany = await _repository.Create(new Company() { Name = viewModel.Name });

			return newCompany;
		}


		public async Task<Company?> Update(CompanyViewModel viewModel)
		{
			await ValidateName(viewModel.Name);

			return await _repository.Update(new Company()
			{
				Id = viewModel.Id.Value,
				Name = viewModel.Name
			});
		}


		public Task<bool> Delete(int id)
		{
			return _repository.Delete(id);
		}


		private async Task<Company> ValidateName(string name)
		{
			var company = await GetByName(name);
			if (company is not null)
			{
				throw new ModelValidationException(nameof(company.Name), "Name is already taken!");
			}

			return company;
		}

	}
}
