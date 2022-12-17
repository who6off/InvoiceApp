using InvoiceApp.Data.Models;
using InvoiceApp.Data.Repositories.Interfaces;
using InvoiceApp.Services.Interfaces;

namespace InvoiceApp.Services
{
	public class StatisticsService : IStatisticsService
	{
		private readonly IInvoiceRepository _invoiceRepository;

		public StatisticsService(IInvoiceRepository invoiceRepository)
		{
			_invoiceRepository = invoiceRepository;
		}

		public Task<List<MonthStatistic>> GetOverallYearStatistics(int year)
		{
			return _invoiceRepository.GetOverallYearStatistic(year);
		}
	}
}
