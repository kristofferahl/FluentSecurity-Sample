using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Web.Models;

namespace Web.Controllers
{
	public class UsersController : Controller
	{
		public ActionResult Index()
		{
			var outModel = new List<UserListModel>();
			var users = Membership.GetAllUsers().OfType<MembershipUser>().OrderBy(x => x.UserName);
			foreach (var user in users)
			{
				var roles = Roles.GetRolesForUser(user.UserName);
				outModel.Add(new UserListModel
				{
					UserName = user.UserName,
					Email = user.Email,
					Roles = roles
				});
			}
			return View(outModel);
		}

		public ActionResult UpdateRoles(string id)
		{
			var userName = id;
			var outModel = new UserRolesEditModel
			{
				Administrator = Roles.IsUserInRole(userName, UserRoles.Administrator),
				Employee = Roles.IsUserInRole(userName, UserRoles.Employee),
				Member = Roles.IsUserInRole(userName, UserRoles.Member),
				UserName = userName
			};
			return View(outModel);
		}

		[HttpPost]
		public ActionResult UpdateRoles(UserRolesEditModel inModel)
		{
			UpdateUserRole(inModel.UserName, UserRoles.Administrator, inModel.Administrator);
			UpdateUserRole(inModel.UserName, UserRoles.Employee, inModel.Employee);
			UpdateUserRole(inModel.UserName, UserRoles.Member, inModel.Member);

			return RedirectToAction("Index");
		}

		private void UpdateUserRole(string userName, string role, bool shouldHaveRole)
		{
			if (shouldHaveRole && Roles.IsUserInRole(userName, role) == false)
				Roles.AddUserToRole(userName, role);

			if (shouldHaveRole == false && Roles.IsUserInRole(userName, role))
				Roles.RemoveUserFromRole(userName, role);
		}
	}
}