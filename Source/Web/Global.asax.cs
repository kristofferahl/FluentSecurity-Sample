using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FluentSecurity;
using FluentSecurity.Configuration;
using Web.App_Start;
using Web.Controllers;

namespace Web
{
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			IoCConfig.Configure();

			AreaRegistration.RegisterAllAreas();

			FilterConfig.RegisterFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);

			SecurityConfigurator.Configure<MvcConfiguration>(configuration =>
			{
				// Let FluentSecurity know how to get the authentication status of the current user
				configuration.GetAuthenticationStatusFrom(() => HttpContext.Current.User.Identity.IsAuthenticated);

				// This is where you set up the policies you want FluentSecurity to enforce on your controllers and actions
				configuration.For<HomeController>().Ignore();
				configuration.For<AccountController>().DenyAuthenticatedAccess();
				configuration.For<AccountController>(x => x.ChangePassword()).DenyAnonymousAccess();
				configuration.For<AccountController>(x => x.LogOff()).DenyAnonymousAccess();
			});

			GlobalFilters.Filters.Add(new HandleSecurityAttribute(), -1);
		}
	}
}
