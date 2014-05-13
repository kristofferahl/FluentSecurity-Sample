namespace Core.Services
{
	public interface IAuthenticator
	{
		bool ValidateCredentials(string username, string password);
	}
}