using System.Web.Mvc;
using FluentSecurity;
using Web.App.Services;

namespace Web.App.Security.ViolationHandlers
{
	public class RequireAnyRolePolicyViolationHandler : IPolicyViolationHandler
	{
		private readonly ILogger _logger;

		public RequireAnyRolePolicyViolationHandler(ILogger logger)
		{
			_logger = logger;
		}

		public ActionResult Handle(PolicyViolationException exception)
		{
			var routeValues = exception.SecurityContext.Data.RouteValues;
			var controllerName = routeValues["controller"];
			var actionName = routeValues["action"];

			_logger.Log("Access denied to {0}Controller {1}.", controllerName, actionName);

			return new AccessDeniedResult(model =>
			{
				if (model.UserIsAuthenticated)
					model.Message = "Sorry, it seems you don't have the required permissions to view this page.";
			});
		}
	}
}