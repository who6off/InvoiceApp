using InvoiceApp.Data.Models;

namespace InvoiceApp.Data.Repositories.Interfaces
{
	public interface IInvoiceRepository
	{
		public Task<Invoice> Create(Invoice model);
	}
}
