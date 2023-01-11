namespace InvoiceApp.Helpers
{
	public static class IQueryCollectionExtensions
	{
		public static string? GetByKey(this IQueryCollection query, string key, string? ifNotFoundValue = null)
		{
			var value = query[key];

			return string.IsNullOrEmpty(value) ? ifNotFoundValue : value;
		}
	}
}
