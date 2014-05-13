using System;
using System.Collections.Generic;
using SisoDb;

namespace Core.Domain.Persistence
{
	public class DomainRepository : IDomainRepository
	{
		private readonly ISisoDatabase _db;

		public DomainRepository(ISisoDatabase db)
		{
			_db = db;
		}

		public T Get<T>(Guid id) where T : AggregateRoot
		{
			using (var uow = _db.BeginSession())
			{
				return uow.GetById<T>(id);
			}
		}

		public IEnumerable<T> GetAll<T>() where T : AggregateRoot
		{
			using (var uow = _db.BeginSession())
			{
				return uow.Query<T>().ToList();
			}
		}

		public void Insert<T>(T aggregateRoot) where T : AggregateRoot
		{
			using (var uow = _db.BeginSession())
			{
				uow.Insert(aggregateRoot);
			}
		}

		public void Update<T>(T aggregateRoot) where T : AggregateRoot
		{
			using (var uow = _db.BeginSession())
			{
				uow.Update(aggregateRoot);
			}
		}

		public void Delete<T>(T aggregateRoot) where T : AggregateRoot
		{
			using (var uow = _db.BeginSession())
			{
				uow.DeleteById<T>(aggregateRoot.Id);
			}
		}
	}
}