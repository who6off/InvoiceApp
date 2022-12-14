﻿using InvoiceApp.Data.Models;
using InvoiceApp.ViewModels.Invoice;

namespace InvoiceApp.Services.Interfaces
{
	public interface IInvoiceService
	{
		public Task<Invoice?> GetById(int id);

		public Task<Invoice?> Create(InvoiceViewModel model);

		public Task<Invoice?> Update(InvoiceViewModel model);
	}
}