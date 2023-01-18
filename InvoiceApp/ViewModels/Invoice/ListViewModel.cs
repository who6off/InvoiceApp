using InvoiceApp.Data.RequestParameters;
using InvoiceApp.Helpers;

namespace InvoiceApp.ViewModels.Invoice
{
    public class ListViewModel
    {
        public string ActionName { get; set; }
        public PagedList<Data.Models.Invoice> Invoices { get; set; }

        public InvoiceRequestParameters Parameters { get; set; }
    }
}
