namespace InvoiceApp.Helpers
{
	static public class ServiceAccessor
	{
		static private IServiceProvider _services = null;

		static public IServiceProvider Services
		{
			get { return _services; }
			set
			{
				if (_services is null)
				{
					_services = value;
				}
			}
		}

		public static T? Get<T>() => _services.GetService<T>();
	}
}
