using InvoiceApp.ViewModels.Components;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceApp.Components
{
	public class Pagination : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(PaginationViewModel viewModel)
		{
			viewModel.Parameters = await viewModel.Parameters.Clone();
			return View(viewModel);
		}
	}
}
