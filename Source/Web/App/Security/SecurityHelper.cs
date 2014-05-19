using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using FluentSecurity;
using FluentSecurity.Configuration;

namespace Web.App.Security
{
	public static class SecurityHelper
	{
		public static bool ActionIsAllowed<TController>(Expression<Func<TController, ActionResult>> actionExpression) where TController : IController
		{
			var actionName = new MvcActionNameResolver().Resolve(actionExpression);
			var fullControllerName = new MvcControllerNameResolver().Resolve(typeof(TController));

			var securityConfiguration = SecurityConfiguration.Get<MvcConfiguration>();
			var policyContainer = securityConfiguration.Runtime.PolicyContainers.GetContainerFor(fullControllerName, actionName);
			if (policyContainer != null)
			{
				var context = securityConfiguration.CreateContext();
				var results = policyContainer.EnforcePolicies(context);

				return results.All(x => x.ViolationOccured == false);
			}

			return true;
		}
	}
}