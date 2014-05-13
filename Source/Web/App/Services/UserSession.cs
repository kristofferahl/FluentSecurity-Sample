using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using Core.Services;

namespace Web.App.Services
{
	public class UserSession : IUserSession
	{
		private MembershipUser GetUser()
		{
			return Membership.GetUser();
		}

		public bool IsAuthenticated()
		{
			return GetUser() != null;
		}

		public string GetUserName()
		{
			return IsAuthenticated() ?
				GetUser().UserName : "";
		}

		public IEnumerable<object> GetRoles()
		{
			return IsAuthenticated() ?
				Roles.GetRolesForUser(GetUserName()) : Enumerable.Empty<object>();
		}
	}
}