using System.Web.Security;
using Core.Services;

namespace Web.App.Services
{
	public class Authenticator : IAuthenticator
	{
		public bool ValidateCredentials(string username, string password)
		{
			return Membership.ValidateUser(username, password);
		}
	}
}