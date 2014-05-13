using System.Web.Mvc;

namespace Web.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Message = "Welcome to BigCompany customer support";

			return View();
		}
	}
}
