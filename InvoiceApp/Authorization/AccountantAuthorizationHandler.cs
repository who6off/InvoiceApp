using InvoiceApp.Data.Models;
using InvoiceApp.Identity.Constants;
using InvoiceApp.Identity.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace InvoiceApp.Authorization
{
	public class AccountantAuthorizationHandler :
		AuthorizationHandler<OperationAuthorizationRequirement, Invoice>
	{
		protected override Task HandleRequirementAsync(
			AuthorizationHandlerContext context,
			OperationAuthorizationRequirement requirement,
			Invoice invoice)
		{
			var user = context.User;

			if ((user is null) || (invoice is null))
			{
				return Task.CompletedTask;
			}

			if (user.GetRole() != UserRoles.Accountant)
			{
				context.Succeed(requirement);
				return Task.CompletedTask;
			}

			if (requirement.Name != InvoiceOperationNames.Create &&
				requirement.Name != InvoiceOperationNames.Read &&
				requirement.Name != InvoiceOperationNames.Delete &&
				requirement.Name != InvoiceOperationNames.Update)
			{
				return Task.CompletedTask;
			}

			if (user.GetId() == invoice.CreatorId)
			{
				context.Succeed(requirement);
			}

			return Task.CompletedTask;
		}
	}
}
