using System.Web.Http.Dependencies;
using StructureMap;

namespace Api.App.ServiceLocation
{
	public class StructureMapDependencyResolver : StructureMapDependencyScope, IDependencyResolver
	{
		public StructureMapDependencyResolver(IContainer container) : base(container) {}

		public IDependencyScope BeginScope()
		{
			var child = Container.GetNestedContainer();
			return new StructureMapDependencyResolver(child);
		}
	}
}