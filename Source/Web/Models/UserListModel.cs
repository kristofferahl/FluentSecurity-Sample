using System.Collections.Generic;
using System.Linq;

namespace Web.Models
{
	public class UserListModel
	{
		public UserListModel()
		{
			Roles = Enumerable.Empty<string>();
		}

		public string UserName { get; set; }
		public string Email { get; set; }
		public IEnumerable<string> Roles { get; set; }
	}
}