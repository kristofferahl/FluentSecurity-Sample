using System.Web.Http;

namespace Api
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			IoCConfig.Configure();
			SecurityConfig.ConfigureSecurity();
			GlobalConfiguration.Configure(WebApiConfig.Register);
		}
	}
}
