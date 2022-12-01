namespace InvoiceApp.Helpers
{
	public static class ConfigurationExtensions
	{
		public static int GetMinEmployeeAge(this IConfiguration config) =>
			config.GetValue<int>("Identity:MinEmployeeAge");
	}
}
