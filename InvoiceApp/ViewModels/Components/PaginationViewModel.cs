using InvoiceApp.Helpers;

namespace InvoiceApp.ViewModels.Components
{
    public class PaginationViewModel
    {
        public string ControllerName { get; set; } = String.Empty;

        public string ActionName { get; set; } = String.Empty;

        public IPagedList Data { get; set; }

        public Dictionary<string, string>? RouteValues { get; set; }
    }
}
