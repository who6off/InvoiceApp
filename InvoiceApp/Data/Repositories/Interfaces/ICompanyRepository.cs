using InvoiceApp.Data.Models;

namespace InvoiceApp.Data.Repositories.Interfaces
{
	public interface ICompanyRepository
	{
		public Task<List<Company>> GetAll();
	}
}
