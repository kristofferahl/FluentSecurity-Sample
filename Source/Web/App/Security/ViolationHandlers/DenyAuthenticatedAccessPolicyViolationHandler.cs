using System.Web.Mvc;
using FluentSecurity;

namespace Web.App.Security.ViolationHandlers
{
	public class DenyAuthenticatedAccessPolicyViolationHandler : IPolicyViolationHandler
	{
		public ActionResult Handle(PolicyViolationException exception)
		{
			return new AccessDeniedResult();
		}
	}
}