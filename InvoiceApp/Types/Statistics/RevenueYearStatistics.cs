using InvoiceApp.Data.Models;

namespace InvoiceApp.Types.Statistics
{
	public class RevenueYearStatistics
	{
		public int Year { get; set; }

		public Company? Company { get; set; }

		public decimal Amount { get; set; }

		public List<MonthData>? Months { get; set; }

		public class MonthData
		{
			public string Month { get; set; }
			public decimal Amount { get; set; }
		}
	}
}
