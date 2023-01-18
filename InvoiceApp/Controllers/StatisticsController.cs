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


		[HttpGet]
		public async Task<IActionResult> Index([FromQuery] StatisticRequestViewModel requestVM, string? returnUrl)
		{
			ViewData["returnUrl"] = returnUrl;

			if (Request.Query.Count < 2)
			{
				return View(new IndexViewModel());
			}

			var statistics = await _statisticsService.GetYearRevenueStatistics(requestVM);

			return View(new IndexViewModel()
			{
				StatisticsRequest = requestVM,
				Statistics = statistics,
			});
		}
	}
}
