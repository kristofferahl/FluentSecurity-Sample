using Core.Services;

namespace Api.App.Services
{
	public class Authenticator : IAuthenticator
	{
		public bool ValidateCredentials(string username, string password)
		{
			return
				username == "apikey" &&
				password == "2c082981-b8df-4516-8467-d328f20a53a1";
		}
	}
}