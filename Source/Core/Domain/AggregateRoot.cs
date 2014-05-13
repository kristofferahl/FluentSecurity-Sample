using System;

namespace Core.Domain
{
	public class AggregateRoot
	{
		public Guid StructureId { get; set; }

		public Guid Id
		{
			get { return StructureId; }
		}
	}
}