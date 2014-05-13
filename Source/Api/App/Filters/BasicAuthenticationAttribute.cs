using System;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Api.App.Filters
{
	public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
	{
		public override void OnAuthorization(HttpActionContext actionContext)
		{
			if (actionContext.Request.Headers.Authorization != null)
			{
				var authToken = actionContext.Request.Headers.Authorization.Parameter;
				var decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));

				var separatorIndex = decodedToken.IndexOf(":", StringComparison.Ordinal);
				if (separatorIndex > 0)
				{
					var username = decodedToken.Substring(0, separatorIndex);
					var password = decodedToken.Substring(separatorIndex + 1);

					if (username == "apikey" && password == "2c082981-b8df-4516-8467-d328f20a53a1")
					{
						var identity = new GenericIdentity("api");
						SetPrincipal(new GenericPrincipal(identity, null));
						base.OnAuthorization(actionContext);
						return;
					}
				}
			}
			actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
		}

		private static void SetPrincipal(IPrincipal principal)
		{
			Thread.CurrentPrincipal = principal;
			if (HttpContext.Current != null)
			{
				HttpContext.Current.User = principal;
			}
		}
	}
}