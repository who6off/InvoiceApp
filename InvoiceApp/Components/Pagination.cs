using InvoiceApp.ViewModels.Components;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceApp.Components
{
    public class Pagination : ViewComponent
    {
        public IViewComponentResult Invoke(PaginationViewModel viewModel)
        {
            viewModel.Parameters.Page = 0;

            return View(viewModel);
        }
    }
}
