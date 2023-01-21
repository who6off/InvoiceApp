using Dapper;
using System.Data;

namespace InvoiceApp.Data
{
	static public class MainDbInitializer
	{
		static public async Task Initialize(IServiceProvider serviceProvider)
		{
			var options = serviceProvider.GetService<IConfiguration>()?
				.GetSection(MainDbInitializerOptions.SectionName)
				.Get<MainDbInitializerOptions>();

			if (!options.IsDataSeedingRequired)
			{
				return;
			}

			var context = serviceProvider.GetService<DapperContext>();
			var hostEnvironment = serviceProvider.GetService<IWebHostEnvironment>();

			var rootPath = hostEnvironment.ContentRootPath;
			var clearScriptPath = Path.Combine(rootPath, options.SqlScriptsFolder, options.ClearScriptFile);
			var schemaScriptPath = Path.Combine(rootPath, options.SqlScriptsFolder, options.SchemaScriptFile);
			var viewsDirectory = Path.Combine(rootPath, options.SqlScriptsFolder, options.ViewsDirectory);

			using (var connection = context.CreateConnection())
			{
				await ExecuteSqlScriptFromFile(connection, clearScriptPath);

				await ExecuteSqlScriptFromFile(connection, schemaScriptPath);

				foreach (var viewScriptName in options.Views)
				{
					var viewScriptPath = Path.Combine(viewsDirectory, viewScriptName);
					await ExecuteSqlScriptFromFile(connection, viewScriptPath);
				}
			}
		}


		static private async Task ExecuteSqlScriptFromFile(IDbConnection connection, string filePath)
		{
			using (var stream = new StreamReader(filePath))
			{
				var sql = await stream.ReadToEndAsync();
				await connection.ExecuteAsync(sql);
			}
		}
	}


	public class MainDbInitializerOptions
	{
		public const string SectionName = "MainDb";

		public bool IsDataSeedingRequired { get; set; }
		public string SqlScriptsFolder { get; set; }
		public string ClearScriptFile { get; set; }
		public string SchemaScriptFile { get; set; }
		public string ViewsDirectory { get; set; }
		public string[] Views { get; set; }
	}
}
