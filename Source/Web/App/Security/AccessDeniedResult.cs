using System;
using System.Dynamic;
using System.Web;
using System.Web.Mvc;

namespace Web.App.Security
{
	public class AccessDeniedResult : ViewResult
	{
		public AccessDeniedResult() : this(null) {}

		public AccessDeniedResult(Action<dynamic> modelModifier)
		{
			dynamic model = new ExpandoObject();

			model.Title = "Access denied";
			model.Message = "Sorry, but you are not allowed to view the page you requested.";
			model.UserIsAuthenticated = HttpContext.Current.User.Identity.IsAuthenticated;
			model.AuthenticationAllowed = !model.UserIsAuthenticated;

			if (modelModifier != null)
				modelModifier.Invoke(model);

			ViewName = "AccessDenied";
			ViewData = new ViewDataDictionary(model);
		}

		public override void ExecuteResult(ControllerContext context)
		{
			context.HttpContext.Response.StatusCode = 403;
			base.ExecuteResult(context);
		}
	}
}