using InvoiceApp.ViewModels.Components;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceApp.Components
{
    public class Pagination : ViewComponent
    {
        public IViewComponentResult Invoke(PaginationViewModel viewModel)
        {
            var queryDictionary = HttpContext.Request.Query
                .ToDictionary(i => i.Key, i => i.Value.ToString());

            if (!queryDictionary.TryAdd("Page", "0"))
                queryDictionary["Page"] = "0";

            viewModel.RouteValues = queryDictionary;

            return View(viewModel);
        }
    }
}
