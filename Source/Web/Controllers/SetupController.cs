using System;
using System.Web.Mvc;
using System.Web.Security;
using SisoDb;
using Web.Models;

namespace Web.Controllers
{
	public class SetupController : Controller
	{
		private readonly ISisoDatabase _database;

		public SetupController(ISisoDatabase database)
		{
			_database = database;
		}

		public ActionResult Index()
		{
			EnsureRolesExist();
			RemoveAllUsersAndAddDefaultUsers();
			_database.EnsureNewDatabase();

			return RedirectToAction("Index", "Home");
		}

		private static void EnsureRolesExist()
		{
			if (!Roles.RoleExists(UserRoles.Member))
				Roles.CreateRole(UserRoles.Member);

			if (!Roles.RoleExists(UserRoles.Employee))
				Roles.CreateRole(UserRoles.Employee);

			if (!Roles.RoleExists(UserRoles.Administrator))
				Roles.CreateRole(UserRoles.Administrator);
		}

		private static void RemoveAllUsersAndAddDefaultUsers()
		{
			var users = Membership.GetAllUsers();
			foreach (MembershipUser user in users)
				Membership.DeleteUser(user.UserName, true);

			AddUser("kristofferahl", UserRoles.Administrator);
			AddUser("chandu", UserRoles.Administrator);
			AddUser("sandra", UserRoles.Employee);
			AddUser("mikael", UserRoles.Member);
		}

		private static void AddUser(string username, string role, string email = "")
		{
			if (String.IsNullOrEmpty(email))
				email = username + "@testmail.com";

			MembershipCreateStatus createStatus;
			Membership.CreateUser(username, "pass123", email, null, null, true, null, out createStatus);
			Roles.AddUserToRole(username, role);
		}
	}
}