using InvoiceApp.Data.Models;
using InvoiceApp.Data.RequestParameters;
using InvoiceApp.Helpers;

namespace InvoiceApp.Services.Interfaces
{
    public interface ICompanyService
    {
        public Task<Company?> GetById(int id);

        public Task<List<Company>> GetAll();

        public Task<PagedList<Company>> Get(CompanyRequestParameters parameters);

        public Task<Company?> Create(string name);

        public Task<Company?> Update(Company company);

        public Task<bool> Delete(int id);
    }
}
