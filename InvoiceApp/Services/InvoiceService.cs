using InvoiceApp.Data.Models;
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


		public async Task<Invoice?> Create(InvoiceViewModel model)
		{
			var company = await _companyRepository.GetByName(model.Owner);
			if (company is null)
				throw new ModelValidationException(nameof(model.Owner), "There is no such a company in the database!");

			var userId = _httpContext?.User.GetId();
			if (string.IsNullOrEmpty(userId))
				return null;

			var date = DateTime.Now;
			var invoice = new Invoice()
			{
				Owner = company,
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
	}
}
