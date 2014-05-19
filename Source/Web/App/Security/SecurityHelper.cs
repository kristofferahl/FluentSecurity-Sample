using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using FluentSecurity;
using FluentSecurity.Configuration;
using FluentSecurity.Core;

namespace Web.App.Security
{
	public static class SecurityHelper
	{
		public static bool ActionIsAllowed<TController>(Expression<Func<TController, ActionResult>> actionExpression) where TController : IController
		{
			var securityConfiguration = SecurityConfiguration.Get<MvcConfiguration>();

			var controllerNameResolver = securityConfiguration.ServiceLocator.Resolve<IControllerNameResolver>();
			var actionNameResolver = securityConfiguration.ServiceLocator.Resolve<IActionNameResolver>();

			var controllerName = controllerNameResolver.Resolve(typeof (TController));
			var actionName = actionNameResolver.Resolve(actionExpression);

			var policyContainer = securityConfiguration.Runtime.PolicyContainers.GetContainerFor(controllerName, actionName);
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