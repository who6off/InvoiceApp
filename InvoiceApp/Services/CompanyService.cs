﻿using InvoiceApp.Data.Models;
using InvoiceApp.Data.Repositories.Interfaces;
using InvoiceApp.Services.Interfaces;

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


		public Task<List<Company>> GetAll()
		{
			return _repository.GetAll();
		}


		public Task<Company> Create(string name)
		{
			return _repository.Create(new Company() { Name = name });
		}


		public Task<Company?> Update(Company company)
		{
			return _repository.Update(company);
		}


		public Task<bool> Delete(int id)
		{
			return _repository.Delete(id);
		}

	}
}