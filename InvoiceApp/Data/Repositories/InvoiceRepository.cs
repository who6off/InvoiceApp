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


		public async Task<Invoice?> GetById(int id)
		{
			var connection = CreateConnection();
			var query = @"
				SELECT * FROM [Invoices] 
				JOIN [Companies]
				ON [Invoices].[OwnerId] = [Companies].[Id]
				WHERE [Invoices].[Id]=@Id
			";
			Invoice? result = null;

			try
			{
				var queryResult = await connection.QueryAsync<Invoice, Company, Invoice>(
					query,
					(invoice, company) =>
					{
						result = invoice;
						result.Owner = company;
						return invoice;
					},
					new { Id = id });
			}
			catch (Exception e)
			{
				return null;
			}

			return result;
		}


		public async Task<Invoice?> Create(Invoice model)
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
				queryResult = await connection.QuerySingleAsync<int>(query, model);
			}
			catch (Exception e)
			{
				return null;
			}

			model.Id = queryResult;

			return model;
		}


		public async Task<Invoice?> Update(Invoice model)
		{
			var connection = CreateConnection();
			var query = @"
				UPDATE [Invoices] SET 
					[OwnerId]=@OwnerId,
					[Amount]=@Amount,
					[Month]=@Month,
					[Status]=(SELECT [Id] FROM [InvoiceStatuses] WHERE [Name]=@Status),
					[LastUpdateDate]=@LastUpdateDate,
					[LastUpdateAction]=(SELECT [Id] FROM [InvoiceActions] WHERE [Name]=@LastUpdateAction),
					[LastUpdateAuthorId]=@LastUpdateAuthorId
				WHERE [Id]=@Id
			";
			int queryResult;

			try
			{
				queryResult = await connection.ExecuteAsync(query, model);
			}
			catch (Exception e)
			{
				return null;
			}

			return (queryResult == 1) ? model : null;
		}
	}
}
