using InvoiceApp.Models.Statistics;
using InvoiceApp.ViewModels.Statistics;

namespace InvoiceApp.Services.Interfaces
{
    public interface IStatisticsService
    {
        public Task<RevenueYearStatistics[]> GetYearRevenueStatistics(StatisticRequestViewModel model);
    }


}
