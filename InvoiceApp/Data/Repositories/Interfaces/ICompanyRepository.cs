using InvoiceApp.Data.Models;

namespace InvoiceApp.Data.Repositories.Interfaces
{
	public interface ICompanyRepository
	{
		public Task<Company?> GetById(int id);

		public Task<Company?> GetByName(string name);

		public Task<List<Company>> GetAll();

		public Task<Company?> Create(Company company);

		public Task<Company?> Update(Company company);

		public Task<bool> Delete(int id);
	}
}
