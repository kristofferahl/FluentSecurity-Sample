using System.Web.Mvc;
using FluentSecurity;

namespace Web
{
	public class FilterConfig
	{
		public static void RegisterFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleSecurityAttribute(), -1);
			filters.Add(new HandleErrorAttribute());
		}
	}
}