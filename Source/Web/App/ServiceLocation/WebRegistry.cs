using Core.Domain.Persistence;
using SisoDb;
using SisoDb.Sql2008;
using StructureMap.Configuration.DSL;

namespace Web.App.ServiceLocation
{
	public class WebRegistry : Registry
	{
		public WebRegistry()
		{
			var cnInfo = new Sql2008ConnectionInfo("FluentSecurity.Sample");
			var instance = new Sql2008DbFactory().CreateDatabase(cnInfo);
			instance.CreateIfNotExists();

			For<ISisoDatabase>().Singleton().Use(instance);

			Scan(scan =>
			{
				scan.AssemblyContainingType<IDomainRepository>();
				scan.AssemblyContainingType<MvcApplication>();
				scan.WithDefaultConventions();
			});
		}
	}
}