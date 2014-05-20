using System.Web.Mvc;
using FluentSecurity;
using Web.App.Services;

namespace Web.App.Security.ViolationHandlers
{
	public class DenyAnonymousAccessPolicyViolationHandler : IPolicyViolationHandler
	{
		private readonly ILogger _logger;

		public DenyAnonymousAccessPolicyViolationHandler(ILogger logger)
		{
			_logger = logger;
		}

		public ActionResult Handle(PolicyViolationException exception)
		{
			var routeValues = exception.SecurityContext.Data.RouteValues;
			var controllerName = routeValues["controller"];
			var actionName = routeValues["action"];

			_logger.Log("Anonymous access denied to {0}Controller {1}.", controllerName, actionName);

			return new AccessDeniedResult();
		}
	}
}