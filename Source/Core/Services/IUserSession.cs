using System.Collections.Generic;

namespace Core.Services
{
	public interface IUserSession
	{
		bool IsAuthenticated();
		string GetUserName();
		IEnumerable<object> GetRoles();
	}
}