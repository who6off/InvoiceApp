using InvoiceApp.Data.Models;
using InvoiceApp.Data.Repositories.Interfaces;
using InvoiceApp.Helpers;
using InvoiceApp.Services.Interfaces;
using InvoiceApp.Models.Statistics;
using InvoiceApp.ViewModels.Statistics;

namespace InvoiceApp.Services
{
	public class StatisticsService : IStatisticsService
	{
		private readonly IInvoiceRepository _invoiceRepository;
		private readonly ICompanyRepository _companyRepository;

		public StatisticsService(
			IInvoiceRepository invoiceRepository,
			ICompanyRepository companyRepository)
		{
			_invoiceRepository = invoiceRepository;
			_companyRepository = companyRepository;
		}


		public async Task<RevenueYearStatistics[]> GetYearRevenueStatistics(StatisticRequestViewModel model)
		{
			var statistics = await _invoiceRepository.GetYearRevenueStatistics(
				model.Year, model.Companies.ToArray());

			foreach (var item in model.Companies)
			{
				if (!statistics.Any(i => i.Company?.Name == item))
				{
					statistics.Add(new MonthStatistics()
					{
						Month = DateTime.Parse($"{model.Year}-01-01"),
						Amount = 0,
						Company = await _companyRepository.GetByName(item)
					});
				}
			}

			if (statistics?.Count == 0 && model.Companies.Count == 0)
			{
				statistics.Add(new MonthStatistics()
				{
					Month = DateTime.Parse($"{model.Year}-01-01"),
					Amount = 0,
					Company = null
				});
			}

			var data = statistics
				.GroupBy(i => i.Company)
				.Select(i => new RevenueYearStatistics()
				{
					Year = model.Year,
					Company = i.Key,
					Amount = i.Sum(i => i.Amount),
					Months = Constants.Months.Select((monthStr, index) =>
					{
						var monthData = i.FirstOrDefault(i => (i.Month.Month - 1) == index);
						var amount = (monthData is null) ? 0 : monthData.Amount;
						return new RevenueYearStatistics.MonthData() { Month = monthStr, Amount = amount };
					}).ToList()
				})
				.ToArray();

			return data;
		}
	}
}
