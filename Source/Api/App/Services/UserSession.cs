using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Core.Services;

namespace Api.App.Services
{
	public class UserSession : IUserSession
	{
		public bool IsAuthenticated()
		{
			return Thread.CurrentPrincipal.Identity.IsAuthenticated;
		}

		public string GetUserName()
		{
			return Thread.CurrentPrincipal.Identity.Name;
		}

		public IEnumerable<object> GetRoles()
		{
			return Enumerable.Empty<object>();
		}
	}
}