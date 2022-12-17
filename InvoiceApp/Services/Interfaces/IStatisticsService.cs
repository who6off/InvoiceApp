using InvoiceApp.Data.Models;

namespace InvoiceApp.Services.Interfaces
{
	public interface IStatisticsService
	{
		public Task<List<MonthStatistic>> GetOverallYearStatistics(int year);
	}


}
