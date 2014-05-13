using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Web.App_Start;

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
		}
	}
}
