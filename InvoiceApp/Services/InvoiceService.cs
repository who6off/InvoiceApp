using InvoiceApp.Data.Models;
using InvoiceApp.Data.Repositories.Interfaces;
using InvoiceApp.Data.RequestParameters;
using InvoiceApp.Helpers;
using InvoiceApp.Helpers.Exceptions;
using InvoiceApp.Identity.Constants;
using InvoiceApp.Identity.Helpers;
using InvoiceApp.Identity.Services.Interfaces;
using InvoiceApp.Services.Interfaces;
using InvoiceApp.ViewModels.Invoice;
using System.Security.Claims;

namespace InvoiceApp.Services
{
	public class InvoiceService : IInvoiceService
	{
		private readonly IInvoiceRepository _invoiceRepository;
		private readonly ICompanyRepository _companyRepository;
		private readonly HttpContext? _httpContext;
		private readonly IUserService _userService;


		public InvoiceService(
			IInvoiceRepository invoiceRepository,
			ICompanyRepository companyRepository,
			IHttpContextAccessor httpContextAccessor,
			IUserService userService)
		{
			_invoiceRepository = invoiceRepository;
			_companyRepository = companyRepository;
			_httpContext = httpContextAccessor.HttpContext;
			_userService = userService;
		}


		public Task<PagedList<Invoice>> Get(InvoiceRequestParameters parameters, ClaimsPrincipal? user = null)
		{
			if ((user is not null) && (user.IsInRole(UserRoles.Accountant)))
			{
				var userId = user.GetId();

				if (!string.IsNullOrEmpty(parameters.UserId) && (parameters.UserId != userId))
				{
					throw new AccessDeniedException("The accountant is not allowed to see invoices of others");
				}

				parameters.UserId = userId;
			}

			return _invoiceRepository.Get(parameters);
		}


		public Task<Invoice?> GetById(int id)
		{
			return _invoiceRepository.GetById(id);
		}

		public async Task<Invoice?> GetByIdWithUserData(int id)
		{
			var invoice = await GetById(id);

			if (invoice is null)
			{
				return null;
			}

			if ((invoice.CreatorId == invoice.LastUpdateAuthorId) && !string.IsNullOrEmpty(invoice.CreatorId))
			{
				invoice.Creator = invoice.LastUpdateAuthor = await _userService.GetById(invoice.CreatorId);
			}
			else
			{
				invoice.Creator = string.IsNullOrEmpty(invoice.CreatorId) ? null : await _userService.GetById(invoice.CreatorId);
				invoice.LastUpdateAuthor = string.IsNullOrEmpty(invoice.LastUpdateAuthorId) ? null : await _userService.GetById(invoice.LastUpdateAuthorId);
			}

			return invoice;
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

		public async Task<Invoice?> Update(InvoiceViewModel model)
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


		public async Task<Invoice?> ChangeStatus(int id, string status)
		{
			if (!InvoiceStatuses.GetAll().Contains(status))
				return null;

			var userId = _httpContext?.User.GetId();
			var date = DateTime.Now;
			var updateAction = status switch
			{
				InvoiceStatuses.Approved => InvoiceActions.Approval,
				InvoiceStatuses.Rejected => InvoiceActions.Rejection,
				_ => throw new NotImplementedException()
			};

			var invoice = new Invoice()
			{
				Id = id,
				Status = status,
				LastUpdateAuthorId = userId,
				LastUpdateAction = updateAction,
				LastUpdateDate = date,
			};

			var updatedInvoice = await _invoiceRepository.Update(invoice);

			return updatedInvoice;
		}


		public Task<bool> Delete(int id)
		{
			return _invoiceRepository.Delete(id);
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
