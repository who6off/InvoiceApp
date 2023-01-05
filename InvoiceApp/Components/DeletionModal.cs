using InvoiceApp.ViewModels.Components;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceApp.Components
{
    public class DeletionModal : ViewComponent
    {
        public IViewComponentResult Invoke(DeletionModalViewModel viewModel)
        {
            return View(viewModel);
        }
    }
}
