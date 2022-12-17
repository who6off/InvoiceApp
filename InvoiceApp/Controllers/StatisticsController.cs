using InvoiceApp.Services.Interfaces;
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
			var statistics = await _statisticsService.GetOverallYearStatistics(2022);
			return View(statistics);
		}
	}
}
