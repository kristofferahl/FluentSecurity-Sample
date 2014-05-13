using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Web.App.Security;

namespace Web.App.Extensions
{
	public static class UrlHelperExtensions
	{
		public static string Action<TController>(this UrlHelper urlHelper, Expression<Func<TController, ActionResult>> actionExpression) where TController : IController
		{
			return urlHelper.Action(actionExpression, null);
		}

		public static string Action<TController>(this UrlHelper urlHelper, Expression<Func<TController, ActionResult>> actionExpression, object values) where TController : IController
		{
			var controllerName = typeof(TController).GetControllerName();
			var actionName = actionExpression.GetActionName();

			var actionIsAllowed = SecurityHelper.ActionIsAllowed(actionExpression);
			if (actionIsAllowed == false)
				return null;

			return urlHelper != null ? urlHelper.Action(actionName, controllerName, values) : "~/";
		}
	}
}