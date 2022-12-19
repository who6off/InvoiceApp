using InvoiceApp.Services.Interfaces;
using InvoiceApp.ViewModels.Statistics;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceApp.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly IStatisticsService _statisticsService;
        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }


        public async Task<IActionResult> Index()
        {
            var statistics = await _statisticsService.GetYearRevenueStatistic(new StatisticRequestViewModel()
            {
                Year = 2022,
                Companies = new string[] { "Nestle", "Skynet" }
            });

            return View(statistics);
        }
    }
}
