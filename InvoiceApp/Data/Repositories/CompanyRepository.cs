using Dapper;
using InvoiceApp.Data.Models;
using InvoiceApp.Data.Repositories.Interfaces;

namespace InvoiceApp.Data.Repositories
{
	public class CompanyRepository : ARepositiry, ICompanyRepository
	{
		public CompanyRepository(DapperContext context) : base(context) { }


		public async Task<Company?> GetById(int id)
		{
			using var connection = CreateConnection();
			var query = "SELECT * FROM [Companies] WHERE [Id]=@Id";
			Company? result;

			try
			{
				result = await connection.QuerySingleAsync<Company?>(query, new { Id = id });
			}
			catch (Exception e)
			{
				return null;
			}

			return result;
		}


		public async Task<Company?> GetByName(string name)
		{
			using var connection = CreateConnection();
			var query = "SELECT * FROM [Companies] WHERE [Name]=@Name";
			Company? result;

			try
			{
				result = await connection.QuerySingleAsync<Company?>(query, new { Name = name });
			}
			catch (Exception e)
			{
				return null;
			}

			return result;
		}


		public async Task<List<Company>> GetAll()
		{
			using var connection = CreateConnection();
			var query = "SELECT * FROM [Companies] ORDER BY [Id] DESC";
			IEnumerable<Company> queryResult;

			try
			{
				queryResult = await connection.QueryAsync<Company>(query);
			}
			catch (Exception e)
			{
				return new List<Company>();
			}

			return queryResult.ToList();
		}


		public async Task<Company?> Create(Company company)
		{
			using var connection = CreateConnection();
			var query = @"
                INSERT INTO [Companies]([Name]) VALUES (@Name);
                SELECT CAST(SCOPE_IDENTITY() as int);
            ";
			int newRecordId;

			try
			{
				newRecordId = await connection.QuerySingleAsync<int>(query, new
				{
					Name = company.Name,
				});
			}
			catch (Exception e)
			{
				return null;
			}

			company.Id = newRecordId;

			return company;
		}


		public async Task<Company?> Update(Company company)
		{
			using var connection = CreateConnection();
			var query = @"
                UPDATE [Companies]
                SET [Name]=@Name
                WHERE [Id]=@Id;
            ";
			int queryResult;

			try
			{
				queryResult = await connection.ExecuteAsync(query, new
				{
					Id = company.Id,
					Name = company.Name
				});
			}
			catch (Exception)
			{
				return null;
			}

			return company;
		}


		public async Task<bool> Delete(int id)
		{
			using var connection = CreateConnection();
			var query = @"
                DELETE FROM [Companies] WHERE [Id]=@Id;
            ";
			int queryResult;

			try
			{
				queryResult = await connection.ExecuteAsync(query, new { Id = id });
			}
			catch (Exception e)
			{
				return false;
			}

			return queryResult != 0;
		}
	}
}
