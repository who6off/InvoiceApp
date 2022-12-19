using InvoiceApp.Data.Repositories.Interfaces;
using InvoiceApp.Helpers;
using InvoiceApp.Services.Interfaces;
using InvoiceApp.Types.Statistics;
using InvoiceApp.ViewModels.Statistics;

namespace InvoiceApp.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public StatisticsService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<RevenueYearStatistic[]> GetYearRevenueStatistic(StatisticRequestViewModel model)
        {
            var statistic = await _invoiceRepository.GetYearRevenueStatistic(
                model.Year, model.Companies);

            var data = statistic
                .GroupBy(i => i.Company)
                .Select(i => new RevenueYearStatistic()
                {
                    Year = model.Year,
                    Company = i.Key,
                    Amount = i.Sum(i => i.Amount),
                    Months = Constants.Months.Select((monthStr, index) =>
                    {
                        var monthData = i.FirstOrDefault(i => (i.Month.Month - 1) == index);
                        var amount = (monthData is null) ? 0 : monthData.Amount;
                        return new RevenueYearStatistic.MonthData() { Month = monthStr, Amount = amount };
                    }).ToList()
                })
                .ToArray();

            return data;
        }
    }
}
