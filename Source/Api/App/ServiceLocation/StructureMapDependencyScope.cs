using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using StructureMap;

namespace Api.App.ServiceLocation
{
	public class StructureMapDependencyScope : IDependencyScope
	{
		protected readonly IContainer Container;

		public StructureMapDependencyScope(IContainer container)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}

			Container = container;
		}

		public void Dispose()
		{
			Container.Dispose();
		}

		public object GetService(Type serviceType)
		{
			if (serviceType == null)
			{
				return null;
			}

			return serviceType.IsAbstract || serviceType.IsInterface
				? Container.TryGetInstance(serviceType)
				: Container.GetInstance(serviceType);
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return Container.GetAllInstances(serviceType).Cast<object>();
		}
	}
}