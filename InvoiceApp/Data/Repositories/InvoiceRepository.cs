using Dapper;
using InvoiceApp.Data.DbViews;
using InvoiceApp.Data.Models;
using InvoiceApp.Data.Repositories.Interfaces;
using InvoiceApp.Data.RequestParameters;
using InvoiceApp.Helpers;

namespace InvoiceApp.Data.Repositories
{
    public class InvoiceRepository : ARepositiry, IInvoiceRepository
    {
        public InvoiceRepository(
            DapperContext context) : base(context)
        { }


        public async Task<List<Invoice>> GetAll()
        {
            using var connection = CreateConnection();
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
            catch (Exception e)
            {
                return new List<Invoice>();
            }

            return result;
        }


        public async Task<PagedList<Invoice>> Get(InvoiceRequestParameters parameters)
        {
            using var connection = CreateConnection();
            var query = $@"
                SELECT 
	                * 
                INTO
	                #TempData
                FROM 
	                [InvoicesView]
                WHERE
                    [InvoicesView].[OwnerName] IS NOT NULL
	                {(string.IsNullOrEmpty(parameters.CompanyName) ? "" : "AND [InvoicesView].[OwnerName]=@CompanyName")}
                    {((parameters.Month is null) ? "" : "AND [InvoicesView].[Month]=@Month")}
                    {((parameters.Status is null) ? "" : "AND [InvoicesView].[Status] in @Status")}
                    {(string.IsNullOrEmpty(parameters.UserId) ? "" : "AND ([InvoicesView].[CreatorId]=@UserId OR [InvoicesView].[LastUpdateAuthorId]=@UserId)")}

                SELECT
	                * 
                FROM
	                #TempData
                ORDER BY 
	                #TempData.[LastUpdateDate] DESC
                OFFSET @Skip ROWS 
                FETCH NEXT @Take ROWS ONLY;

                SELECT COUNT(*) FROM #TempData;
            ";

            IEnumerable<Invoice> source = new Invoice[] { };
            int totalCount;

            try
            {
                using (var multi = await connection.QueryMultipleAsync(query, new
                {
                    CompanyName = parameters.CompanyName,
                    Month = parameters.Month,
                    Status = parameters.Status,
                    UserId = parameters.UserId,
                    Skip = (int)parameters.Page * parameters.PageSize,
                    Take = (int)parameters.PageSize
                }))
                {
                    source = (await multi.ReadAsync<InvoiceDbView>()).Select(i => new Invoice(i));
                    totalCount = await multi.ReadSingleAsync<int>();
                }
            }
            catch (Exception e)
            {
                return new PagedList<Invoice>(source, 0, parameters.PageSize, 0);
            }

            return new PagedList<Invoice>(source, parameters.Page, parameters.PageSize, totalCount);
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
            var query = $@"
				UPDATE [Invoices] SET 
					{((model.OwnerId == default(int)) ? "" : "[OwnerId] = @OwnerId,")}
                    {((model.Amount == default(decimal)) ? "" : "[Amount] = @Amount,")}
                    {((model.Month == default(DateTime)) ? "" : "[Month]= @Month,")}
					[Status]= (SELECT[Id] FROM[InvoiceStatuses] WHERE[Name] = @Status),
					[LastUpdateDate]= @LastUpdateDate,
					[LastUpdateAction]= (SELECT[Id] FROM[InvoiceActions] WHERE[Name] = @LastUpdateAction),
					[LastUpdateAuthorId]= @LastUpdateAuthorId
                WHERE[Id] = @Id
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


        public async Task<List<MonthStatistics>> GetYearRevenueStatistics(int year, string[] companies = null)
        {
            using var connection = CreateConnection();
            var isCompaniesRequested = (companies is not null) && (companies.Length > 0);
            var query = $@"
                SELECT 
	                [I].[Month],
                    {((isCompaniesRequested) ? "[C].[Id], [C].[Name]," : "")}
	                SUM([I].[Amount]) AS [Amount]
                FROM 
	                [Invoices] AS [I]
                JOIN 
	                [Companies] AS [C]
                ON
	                [I].[OwnerId]=[C].[Id]
                WHERE 
	                YEAR([I].[Month])=@Year 
                    {((isCompaniesRequested) ? "AND [C].[Name] IN @Companies" : "")}
                GROUP BY 
	                [I].[Month]
                    {((isCompaniesRequested) ? ", [C].[Id], [C].[Name]" : "")}
            ";
            List<MonthStatistics> result;

            try
            {
                if (isCompaniesRequested)
                {
                    var queryResult = await connection.QueryAsync<(DateTime Month, int Id, string Name, decimal Amount)>(
                        query, new { Year = year, Companies = companies });
                    result = queryResult
                        .Select(i => new MonthStatistics()
                        {
                            Month = i.Month,
                            Amount = i.Amount,
                            Company = new Company() { Id = i.Id, Name = i.Name }
                        })
                        .ToList();
                }
                else
                {
                    result = (await connection.QueryAsync<MonthStatistics>(
                          query,
                          new { Year = year })
                        )
                        .ToList() ?? new List<MonthStatistics>();
                }
            }
            catch (Exception e)
            {
                return new List<MonthStatistics>();
            }

            return result;
        }
    }
}
