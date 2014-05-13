using System;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Core.Services;

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

					var authenticator = (IAuthenticator) GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IAuthenticator));

					if (authenticator.ValidateCredentials(username, password))
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