using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
	public class IssueModels
	{
		public class Details
		{
			public Guid Id { get; set; }
			public bool Open { get; set; }
			public string Subject { get; set; }
			public string Text { get; set; }
			public DateTime Submitted { get; set; }
			public DateTime? Modified { get; set; }
			public string User { get; set; }
		}

		public class Post
		{
			[Required]
			public string Subject { get; set; }
			
			[Required]
			public string Text { get; set; }

			[Required]
			public DateTime Submitted { get; set; }
		}

		public class Put
		{
			[Required]
			public string Subject { get; set; }

			[Required]
			public string Text { get; set; }

			[Required]
			public DateTime Modified { get; set; }
		}
	}
}