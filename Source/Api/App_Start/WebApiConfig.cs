using System.Web.Http;
using Api.App.Filters;
using FluentSecurity.WebApi;

namespace Api
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services
			config.Filters.Add(new BasicAuthenticationAttribute());
			config.Filters.Add(new HandleSecurityAttribute());
			config.Filters.Add(new ApiValidationActionFilter());

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}
