using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.ViewModels.Company
{
    public class CompanyViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Input the name")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Input the name")]
        public string Name { get; set; }
    }
}
