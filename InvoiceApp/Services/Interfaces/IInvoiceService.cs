using InvoiceApp.Data.Models;
using InvoiceApp.Data.RequestParameters;
using InvoiceApp.Helpers;
using InvoiceApp.ViewModels.Invoice;

namespace InvoiceApp.Services.Interfaces
{
	public interface IInvoiceService
	{
		public Task<PagedList<Invoice>> Get(InvoiceRequestParemeters paremeters);

		public Task<Invoice?> GetById(int id);

		public Task<Invoice?> Create(InvoiceViewModel model);

		public Task<Invoice?> Update(InvoiceViewModel model);

		public Task<bool> Delete(int id);

		public Task<Invoice?> ChangeStatus(int id, string status);

	}
}
