using InvoiceApp.Data.Models;

namespace InvoiceApp.Services.Interfaces
{
	public interface ICompanyService
	{
		public Task<List<Company>> GetAll();

		public Task<bool> Create(string name);

	}
}
