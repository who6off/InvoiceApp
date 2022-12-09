﻿using Dapper;
using InvoiceApp.Data.Models;
using InvoiceApp.Data.Repositories.Interfaces;

namespace InvoiceApp.Data.Repositories
{
    public class CompanyRepository : ARepositiry, ICompanyRepository
    {
        public CompanyRepository(DapperContext context) : base(context) { }


        public async Task<List<Company>> GetAll()
        {
            var query = "SELECT * FROM [Companies] ORDER BY [Id] DESC";

            using (var connection = _context.CreateConnection())
            {
                var queryResult = await connection.QueryAsync<Company>(query);
                return queryResult.ToList();
            }
        }


        public async Task<bool> Create(Company company)
        {
            var query = "INSERT INTO [Companies]([Name]) VALUES (@Name)";

            using (var connection = _context.CreateConnection())
            {
                var queryResult = await connection.ExecuteAsync(query, new
                {
                    Name = company.Name,
                });

                return queryResult != 0;
            }
        }
    }
}
