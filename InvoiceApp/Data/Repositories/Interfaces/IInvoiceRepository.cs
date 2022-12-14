using InvoiceApp.Data.Models;

namespace InvoiceApp.Data.Repositories.Interfaces
{
    public interface IInvoiceRepository
    {
        public Task<Invoice?> GetById(int id);

        public Task<List<Invoice>> GetAll();

        public Task<Invoice?> Create(Invoice model);

        public Task<Invoice?> Update(Invoice model);
    }
}
