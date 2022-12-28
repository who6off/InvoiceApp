using InvoiceApp.Data.Models;
using InvoiceApp.Data.RequestParameters;
using InvoiceApp.Helpers;

namespace InvoiceApp.Data.Repositories.Interfaces
{
    public interface IInvoiceRepository
    {
        public Task<Invoice?> GetById(int id);

        public Task<List<Invoice>> GetAll();

        public Task<PagedList<Invoice>> Get(InvoiceRequestParemeters parameners);

        public Task<Invoice?> Create(Invoice model);

        public Task<Invoice?> Update(Invoice model);

        public Task<bool> Delete(int id);

        public Task<List<MonthStatistics>> GetYearRevenueStatistics(int year, string[] companies = null);
    }
}
