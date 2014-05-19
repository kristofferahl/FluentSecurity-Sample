using System.Net;
using System.Net.Http;
using FluentSecurity;
using FluentSecurity.WebApi.Policy.ViolationHandlers;

namespace Api.App.Security.ViolationHandlers
{
	public class DefaultPolicyViolationHandler : IWebApiPolicyViolationHandler
	{
		public object Handle(PolicyViolationException exception)
		{
			return new HttpResponseMessage(HttpStatusCode.Unauthorized)
			{
				Content = new StringContent("Access denied!")
			};
		}
	}
}