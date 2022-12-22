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
			return View(new IndexViewModel());
		}


		[HttpPost]
		public async Task<IActionResult> Index(IndexViewModel model)
		{
			var statistics = await _statisticsService.GetYearRevenueStatistics(model.StatisticsRequest);

			model.Statistics = statistics;

			return View(model);
		}
	}
}
