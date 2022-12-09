using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.ViewModels.Company
{
    public class IndexViewModel
    {
        [Required(ErrorMessage = "Input the name")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Input the name")]
        public string NewCompanyName { get; set; }

        public List<Data.Models.Company>? Companies { get; set; }
    }
}
