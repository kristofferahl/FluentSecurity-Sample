using System.Web.Http;
using Api.App.ServiceLocation;
using StructureMap;
using StructureMap.Graph;

namespace Api
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
			GlobalConfiguration.Configuration.DependencyResolver = new StructureMapDependencyResolver(container);
		} 
	}
}