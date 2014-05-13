using System.Web.Http;

namespace Api
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			IoCConfig.Configure();

			GlobalConfiguration.Configure(WebApiConfig.Register);
		}
	}
}
