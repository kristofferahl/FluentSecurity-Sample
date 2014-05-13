using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Web.App.Extensions
{
	public static class MvcExtensions
	{
		public static string GetControllerName(this Type controllerType)
		{
			return controllerType.Name.Replace("Controller", string.Empty);
		}

		public static string GetFullControllerName(this Type controllerType)
		{
			return controllerType.FullName;
		}

		public static string GetActionName<TController>(this Expression<Func<TController, ActionResult>> actionExpression) where TController : IController
		{
			return ((MethodCallExpression)actionExpression.Body).Method.Name;
		}
	}
}