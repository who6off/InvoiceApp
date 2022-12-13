namespace InvoiceApp.Helpers
{
	public class ModelValidationException : Exception
	{
		public string Propery { get; }
		public ModelValidationException(string property, string message) : base(message)
		{
			Propery = property;
		}
	}
}
