using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Json;

namespace InvoiceApp.Helpers
{
	public static class TempDataExtensions
	{
		public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
		{
			tempData[key] = JsonSerializer.Serialize<T>(value);
		}

		public static T? Get<T>(this ITempDataDictionary tempData, string key) where T : class
		{
			;
			if (!tempData.TryGetValue(key, out object json))
				return null;

			return JsonSerializer.Deserialize<T>((string)json);
		}
	}
}
