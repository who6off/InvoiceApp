using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace InvoiceApp.Authorization
{
	public class InvoiceOperations
	{
		public static readonly OperationAuthorizationRequirement Create =
			new() { Name = InvoiceOperationNames.Create };

		public static readonly OperationAuthorizationRequirement Read =
			new() { Name = InvoiceOperationNames.Read };

		public static readonly OperationAuthorizationRequirement Update =
			new() { Name = InvoiceOperationNames.Update };

		public static readonly OperationAuthorizationRequirement Delete =
			new() { Name = InvoiceOperationNames.Delete };

		public static readonly OperationAuthorizationRequirement Approve =
			new() { Name = InvoiceOperationNames.Approve };

		public static readonly OperationAuthorizationRequirement Reject =
			new() { Name = InvoiceOperationNames.Reject };
	}


	static public class InvoiceOperationNames
	{
		public const string Create = "Create";
		public const string Read = "Read";
		public const string Update = "Update";
		public const string Delete = "Delete";
		public const string Approve = "Approve";
		public const string Reject = "Reject";
	}
}
