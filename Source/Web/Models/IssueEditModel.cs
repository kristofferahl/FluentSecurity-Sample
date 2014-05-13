using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
	public class IssueEditModel
	{
		[Required]
		public string Subject { get; set; }
		
		[Required]
		public string Text { get; set; }
	}
}