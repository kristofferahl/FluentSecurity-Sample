using System;

namespace Core.Domain
{
	public class Issue : AggregateRoot
	{
		protected Issue() {}

		public Issue(string user)
		{
			Submitted = DateTime.Now;
			User = user;
			Open = true;
		}

		public string Subject { get; set; }
		public string Text { get; set; }
		public DateTime Submitted { get; set; }
		public DateTime? Modified { get; set; }
		public string User { get; set; }
		public bool Open { get; set; }
	}
}