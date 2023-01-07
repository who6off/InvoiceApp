using InvoiceApp.Data.Models;
using InvoiceApp.Data.RequestParameters;
using InvoiceApp.Helpers;

namespace InvoiceApp.Data.Repositories.Interfaces
{
    public interface ICompanyRepository
    {
        public Task<Company?> GetById(int id);

        public Task<Company?> GetByName(string name);

        public Task<List<Company>> GetAll();

        public Task<PagedList<Company>> Get(CompanyRequestParameters parameters);

        public Task<Company?> Create(Company company);

        public Task<Company?> Update(Company company);

        public Task<bool> Delete(int id);
    }
}
