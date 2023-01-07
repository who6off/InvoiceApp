using InvoiceApp.Data.RequestParameters;
using InvoiceApp.Helpers;

namespace InvoiceApp.ViewModels.Company
{
    public class IndexViewModel
    {
        public PagedList<Data.Models.Company> Companies { get; set; }

        public CompanyRequestParameters Parameters { get; set; }
    }
}
