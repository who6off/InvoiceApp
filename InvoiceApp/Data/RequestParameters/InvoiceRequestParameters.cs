﻿namespace InvoiceApp.Data.RequestParameters
{
	public class InvoiceRequestParameters : ARequestParameters
	{
		public string? CompanyName { get; set; }

		public string[]? Status { get; set; }

		public DateTime? Month { get; set; }

		public string? UserId { get; set; }


		public InvoiceRequestParameters() : base() { }
	}
}
