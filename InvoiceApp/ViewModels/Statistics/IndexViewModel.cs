namespace InvoiceApp.ViewModels.Statistics
{
    public class StatisticRequestViewModel
    {
        public int Year { get; set; } = DateTime.Now.Year;

        public string[]? Companies { get; set; }
    }
}
