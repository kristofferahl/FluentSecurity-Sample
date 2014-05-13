using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Web.App.Security
{
	public static class SecurityHelper
	{
		public static bool ActionIsAllowed<TController>(Expression<Func<TController, ActionResult>> actionExpression) where TController : IController
		{
			return true;
		}
	}
}