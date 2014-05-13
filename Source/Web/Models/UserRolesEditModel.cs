using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
	public class UserRolesEditModel
	{
		[Required]
		public string UserName { get; set; }

		public bool Administrator { get; set; }
		public bool Employee { get; set; }
		public bool Member { get; set; }
	}
}