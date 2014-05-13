using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Api.App.Filters
{
	public class ApiValidationActionFilter : ActionFilterAttribute
	{
		public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
		{
			var modelState = actionContext.ModelState;
			if (!modelState.IsValid)
			{
				actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
			}
			else
			{
				base.OnActionExecuting(actionContext);
			}
		}
	}
}