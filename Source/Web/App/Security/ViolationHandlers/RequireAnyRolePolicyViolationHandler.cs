using System.Web.Mvc;
using FluentSecurity;

namespace Web.App.Security.ViolationHandlers
{
	public class RequireAnyRolePolicyViolationHandler : IPolicyViolationHandler
	{
		public ActionResult Handle(PolicyViolationException exception)
		{
			return new AccessDeniedResult(model =>
			{
				if (model.UserIsAuthenticated)
					model.Message = "Sorry, it seems you don't have the required permissions to view this page.";
			});
		}
	}
}