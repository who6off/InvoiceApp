using InvoiceApp.Data.Models;
using InvoiceApp.Data.RequestParameters;
using InvoiceApp.Helpers;
using InvoiceApp.ViewModels.Invoice;
using System.Security.Claims;

namespace InvoiceApp.Services.Interfaces
{
    public interface IInvoiceService
    {
        public Task<PagedList<Invoice>> Get(InvoiceRequestParameters parameters, ClaimsPrincipal? user = null);

        public Task<Invoice?> GetById(int id);

        public Task<Invoice?> GetByIdWithUserData(int id);

        public Task<Invoice?> Create(InvoiceViewModel model);

        public Task<Invoice?> Update(InvoiceViewModel model);

        public Task<bool> Delete(int id);

        public Task<Invoice?> ChangeStatus(int id, string status);

    }
}
