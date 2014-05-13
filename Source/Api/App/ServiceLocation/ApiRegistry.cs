using Core.Domain.Persistence;
using SisoDb;
using SisoDb.Sql2008;
using StructureMap.Configuration.DSL;

namespace Api.App.ServiceLocation
{
	public class ApiRegistry : Registry
	{
		public ApiRegistry()
		{
			var cnInfo = new Sql2008ConnectionInfo("FluentSecurity.Sample");
			var instance = new Sql2008DbFactory().CreateDatabase(cnInfo);
			instance.CreateIfNotExists();

			For<ISisoDatabase>().Singleton().Use(instance);

			Scan(scan =>
			{
				scan.AssemblyContainingType<IDomainRepository>();
				scan.AssemblyContainingType<WebApiApplication>();
				scan.WithDefaultConventions();
			});
		}
	}
}