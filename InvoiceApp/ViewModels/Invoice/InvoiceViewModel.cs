using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.ViewModels.Invoice
{
	public class InvoiceViewModel
	{
		public int? Id { get; set; }

		public string Owner { get; set; }

		[Required(ErrorMessage = "Input the amount of invoice")]
		public decimal Amount { get; set; }

		[Required(ErrorMessage = "Select the month of invoice")]
		public DateTime Month { get; set; } = DateTime.Now;
	}
}
