using InvoiceApp.Types.Statistics;

namespace InvoiceApp.ViewModels.Statistics
{
	public class IndexViewModel
	{
		public StatisticRequestViewModel StatisticsRequest { get; set; } = new();

		public RevenueYearStatistics[] Statistics { get; set; }
	}

	public class StatisticRequestViewModel
	{
		public int Year { get; set; } = DateTime.Now.Year;

		public List<string> Companies { get; set; } = new List<string>();
	}
}
