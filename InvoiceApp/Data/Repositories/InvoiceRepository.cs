using Dapper;
using InvoiceApp.Data.DbViews;
using InvoiceApp.Data.Models;
using InvoiceApp.Data.Repositories.Interfaces;

namespace InvoiceApp.Data.Repositories
{
    public class InvoiceRepository : ARepositiry, IInvoiceRepository
    {
        public InvoiceRepository(
            DapperContext context) : base(context)
        { }


        public async Task<List<Invoice>> GetAll()
        {
            var connection = CreateConnection();
            var query = @"
                SELECT * FROM [InvoicesView]
                ORDER BY [InvoicesView].[LastUpdateDate] DESC
            ";
            var result = new List<Invoice>();

            try
            {
                result = (await connection.QueryAsync<InvoiceDbView>(query))
                    .Select(i => new Invoice(i))
                    .ToList();
            }
            catch (Exception)
            {
                return new List<Invoice>();
            }

            return result;
        }


        public async Task<Invoice?> GetById(int id)
        {
            using var connection = CreateConnection();
            var query = @"
				SELECT * FROM [InvoicesView] 
				WHERE [InvoicesView].[Id]=@Id
			";
            InvoiceDbView queryResult = null;

            try
            {
                queryResult = await connection.QuerySingleAsync<InvoiceDbView>(query, new { Id = id });
            }
            catch (Exception e)
            {
                return null;
            }

            return (queryResult is null) ? null : new Invoice(queryResult);
        }


        public async Task<Invoice?> Create(Invoice model)
        {
            using var connection = CreateConnection();
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
            using var connection = CreateConnection();
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


        public async Task<bool> Delete(int id)
        {
            using var connection = CreateConnection();
            var query = @"
                DELETE FROM [Invoices] WHERE [Id]=@id
            ";
            int queryResult = 0;

            try
            {
                queryResult = await connection.ExecuteAsync(query, new { Id = id });
            }
            catch (Exception e)
            {
                return false;
            }

            return queryResult == 1;
        }



    }
}
