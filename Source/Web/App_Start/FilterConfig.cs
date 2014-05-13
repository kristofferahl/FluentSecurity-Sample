using System.Web.Mvc;

namespace Web
{
	public class FilterConfig
	{
		public static void RegisterFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}