using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using FluentSecurity;
using FluentSecurity.Configuration;
using Web.App.Security;
using Web.App_Start;
using Web.Controllers;
using Web.Models;

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

				// Let FluentSecurity know how to get the roles for the current user
				configuration.GetRolesFrom(() => Roles.GetRolesForUser(HttpContext.Current.User.Identity.Name));

				// This is where you set up the policies you want Fluent Security to enforce on your controllers and actions
				configuration.ForAllControllers().RequireAnyRole(UserRoles.Administrator);

				configuration.For<HomeController>().Ignore();

				configuration.For<AccountController>().DenyAuthenticatedAccess();
				configuration.For<AccountController>(x => x.ChangePassword()).DenyAnonymousAccess();
				configuration.For<AccountController>(x => x.ChangePasswordSuccess()).DenyAnonymousAccess();
				configuration.For<AccountController>(x => x.LogOff()).DenyAnonymousAccess();

				configuration.For<IssuesController>(x => x.Index()).DenyAnonymousAccess();
				configuration.For<IssuesController>(x => x.Details(Guid.Empty)).DenyAnonymousAccess();
				configuration.For<IssuesController>(x => x.Create()).RequireAnyRole(UserRoles.Member);
				configuration.For<IssuesController>(x => x.Close(Guid.Empty)).RequireAnyRole(UserRoles.Employee, UserRoles.Administrator);

				configuration.For<UsersController>().RequireAnyRole(UserRoles.Administrator);

				configuration.For<SetupController>().AddPolicy(new LocalAccessPolicy());
			});

			GlobalFilters.Filters.Add(new HandleSecurityAttribute(), -1);
		}
	}
}
