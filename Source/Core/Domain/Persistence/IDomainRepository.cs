using System;
using System.Collections.Generic;

namespace Core.Domain.Persistence
{
	public interface IDomainRepository
	{
		T Get<T>(Guid id) where T : AggregateRoot;
		IEnumerable<T> GetAll<T>() where T : AggregateRoot;
		void Insert<T>(T aggregateRoot) where T : AggregateRoot;
		void Update<T>(T aggregateRoot) where T : AggregateRoot;
		void Delete<T>(T aggregateRoot) where T : AggregateRoot;
	}
}