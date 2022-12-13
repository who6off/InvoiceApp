using Dapper;
using InvoiceApp.Data.Models;
using InvoiceApp.Data.Repositories.Interfaces;

namespace InvoiceApp.Data.Repositories
{
	public class InvoiceRepository : ARepositiry, IInvoiceRepository
	{
		public InvoiceRepository(
			DapperContext context) : base(context)
		{ }


		public async Task<Invoice> Create(Invoice model)
		{
			var connection = CreateConnection();
			var query = @"
				INSERT INTO [Invoices] VALUES
				(
					@OwnerId,
					@Amount,
					@Month,
					@CreationDate,
					@CreatorId, 
					(SELECT [Id] FROM [InvoiceStatuses] WHERE [Name]=@Status),
					@LastUpdateDate,
					(SELECT [Id] FROM [InvoiceActions] WHERE [Name]=@LastUpdateAction),
					@LastUpdateAuthorId
				);
				SELECT CAST(SCOPE_IDENTITY() as int);
			";
			int queryResult;

			try
			{
				//queryResult = await connection.QuerySingleAsync<int>(query, new
				//{
				//	OwnerId = model.Owner.Id,
				//	Amount = model.Amount,
				//	Month = model.Month,
				//	CreationDate = model.CreationDate,
				//	CreatorId = model.CreatorId,
				//	Status = model.Status,
				//	LastUpdateDate = model.LastUpdateDate,
				//	LastUpdateAction = model.LastUpdateAction,
				//	LastUpdateAuthorId = model.LastUpdateAuthorId,
				//});
				queryResult = await connection.QuerySingleAsync<int>(query, model);
			}
			catch (Exception e)
			{
				return null;
			}

			model.Id = queryResult;

			return model;
		}
	}
}
