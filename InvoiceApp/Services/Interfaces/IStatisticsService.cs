using InvoiceApp.Types.Statistics;
using InvoiceApp.ViewModels.Statistics;

namespace InvoiceApp.Services.Interfaces
{
    public interface IStatisticsService
    {
        public Task<RevenueYearStatistics[]> GetYearRevenueStatistics(StatisticRequestViewModel model);
    }


}
