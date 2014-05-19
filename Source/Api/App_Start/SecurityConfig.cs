using System.Diagnostics;
using System.Threading;
using System.Web.Http;
using Api.Controllers;
using FluentSecurity;
using FluentSecurity.WebApi.Configuration;

namespace Api
{
	public class SecurityConfig
	{
		public static ISecurityConfiguration ConfigureSecurity()
		{
			var config = SecurityConfigurator.Configure<WebApiConfiguration>(configuration =>
			{
				// Tell FluentSecurity how to resolve services
				configuration.ResolveServicesUsing(GlobalConfiguration.Configuration.DependencyResolver.GetServices);

				// Let FluentSecurity know how to get the authentication status of the current user
				configuration.GetAuthenticationStatusFrom(() => Thread.CurrentPrincipal.Identity.IsAuthenticated);

				// This is where you set up the policies you want FluentSecurity to enforce on your controllers and actions
				configuration.For<IssuesController>().DenyAnonymousAccess();
			});

			Debug.Write(config.WhatDoIHave());

			return config;
		}
	}
}