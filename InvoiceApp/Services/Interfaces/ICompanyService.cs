using InvoiceApp.Data.Models;

namespace InvoiceApp.Services.Interfaces
{
    public interface ICompanyService
    {
        public Task<Company?> GetById(int id);

        public Task<List<Company>> GetAll();

        public Task<Company?> Create(string name);

        public Task<Company?> Update(Company company);

        public Task<bool> Delete(int id);
    }
}
