namespace InvoiceApp.ViewModels.Components
{
    public class PaginationViewModel
    {
        public string ControllerName { get; set; } = String.Empty;

        public string ActionName { get; set; } = String.Empty;

        public uint CurrentPage { get; set; }

        public uint PagesAmount { get; set; }

        public Dictionary<string, string>? RouteValues { get; set; }
    }
}
