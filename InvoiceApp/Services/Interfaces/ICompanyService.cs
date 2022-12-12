using InvoiceApp.Data.Models;

namespace InvoiceApp.Services.Interfaces
{
	public interface ICompanyService
	{
		public Task<List<Company>> GetAll();

		public Task<Company> Create(string name);

		public Task<bool> Delete(int id);
	}
}
