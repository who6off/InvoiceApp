namespace InvoiceApp.Data.Models
{
    public class MonthStatistics
    {
        public Company? Company { get; set; }

        public DateTime Month { get; set; }

        public decimal Amount { get; set; }
    }
}
