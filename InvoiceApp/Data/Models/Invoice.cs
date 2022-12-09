namespace InvoiceApp.Data.Models
{
	public class Invoice
	{
		public int Id { get; set; }

		public int? OwnerId { get; set; }

		public Company? Owner { get; set; }

		public decimal Amount { get; set; }

		public DateTime Month { get; set; }

		public string? CreatorId { get; set; }

		public DateTime CreationDate { get; set; }

		public string Status { get; set; }

		public string LastUpdateAction { get; set; }

		public DateTime LastUpdateTime { get; set; }

		public string? LastUpdateAuthorId { get; set; }
	}


	static public class InvoiceStatuses
	{
		public const string Submitted = "Submitted";
		public const string Approved = "Approved";
		public const string Rejected = "Rejected";
	}


	static public class InvoiceActions
	{
		public const string Creation = "Creation";
		public const string Update = "Update";
		public const string Approval = "Approval";
		public const string Rejection = "Rejection";
	}
}
