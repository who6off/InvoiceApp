﻿using InvoiceApp.Data.Models;
using InvoiceApp.Data.Repositories.Interfaces;
using InvoiceApp.Helpers;
using InvoiceApp.Identity.Helpers;
using InvoiceApp.Services.Interfaces;
using InvoiceApp.ViewModels.Invoice;

namespace InvoiceApp.Services
{
	public class InvoiceService : IInvoiceService
	{
		private readonly IInvoiceRepository _invoiceRepository;
		private readonly ICompanyRepository _companyRepository;
		private readonly HttpContext? _httpContext;

		public InvoiceService(
			IInvoiceRepository invoiceRepository,
			ICompanyRepository companyRepository,
			IHttpContextAccessor httpContextAccessor)
		{
			_invoiceRepository = invoiceRepository;
			_companyRepository = companyRepository;
			_httpContext = httpContextAccessor.HttpContext;
		}


		public Task<Invoice?> GetById(int id)
		{
			return _invoiceRepository.GetById(id);
		}


		public async Task<Invoice?> Create(InvoiceViewModel model)
		{
			var validationResult = await ValidateInvoiceViewModel(model);
			var userId = _httpContext?.User.GetId();

			var date = DateTime.Now;
			var invoice = new Invoice()
			{
				Owner = validationResult.Company,
				Amount = model.Amount,
				Month = model.Month,
				Status = InvoiceStatuses.Submitted,
				CreatorId = userId,
				CreationDate = date,
				LastUpdateAuthorId = userId,
				LastUpdateAction = InvoiceActions.Creation,
				LastUpdateDate = date,
			};

			var newInvoice = await _invoiceRepository.Create(invoice);

			return newInvoice;
		}

		public async Task<Invoice> Update(InvoiceViewModel model)
		{
			var validationResult = await ValidateInvoiceViewModel(model);
			var userId = _httpContext?.User.GetId();

			var date = DateTime.Now;
			var invoice = new Invoice()
			{
				Id = model.Id.Value,
				Owner = validationResult.Company,
				Amount = model.Amount,
				Month = model.Month,
				Status = InvoiceStatuses.Submitted,
				LastUpdateAuthorId = userId,
				LastUpdateAction = InvoiceActions.Update,
				LastUpdateDate = date,
			};

			var updatedInvoice = await _invoiceRepository.Update(invoice);

			return updatedInvoice;
		}


		private async Task<InvoiceViewModelValidationResult> ValidateInvoiceViewModel(InvoiceViewModel model)
		{
			var company = await _companyRepository.GetByName(model.Owner);
			if (company is null)
				throw new ModelValidationException(nameof(model.Owner), "There is no such a company in the database!");

			return new InvoiceViewModelValidationResult()
			{
				Company = company
			};
		}

		private class InvoiceViewModelValidationResult
		{
			public Company? Company { get; set; }
		}
	}
}
