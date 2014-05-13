using System.Web.Mvc;
using StructureMap;
using StructureMap.Graph;
using Web.App.ServiceLocation;

namespace Web.App_Start
{
	public class IoCConfig
	{
		public static void Configure()
		{
			ObjectFactory.Initialize(x => x.Scan(scan =>
			{
				scan.TheCallingAssembly();
				scan.LookForRegistries();
			}));
			var container = ObjectFactory.Container;
			DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
		} 
	}
}