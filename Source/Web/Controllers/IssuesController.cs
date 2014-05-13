using System;
using System.Linq;
using System.Web.Mvc;
using Core.Domain;
using Core.Domain.Persistence;
using Core.Services;
using Web.Models;

namespace Web.Controllers
{
	public class IssuesController : Controller
	{
		private readonly IUserSession _userSession;
		private readonly IDomainRepository _repository;

		public IssuesController(IUserSession userSession, IDomainRepository repository)
		{
			_userSession = userSession;
			_repository = repository;
		}

		public ActionResult Index()
		{
			var savedIssues = _repository.GetAll<Issue>().ToList();
			var userRoles = (_userSession.GetRoles() ?? Enumerable.Empty<object>()).ToList();

			var showAll = userRoles.Contains(UserRoles.Administrator) || userRoles.Contains(UserRoles.Employee);
			if (showAll == false)
				savedIssues = savedIssues.Where(x => x.User == _userSession.GetUserName()).ToList();

			return View(savedIssues);
		}

		public ActionResult Create()
		{
			return View(new IssueEditModel());
		}

		[HttpPost]
		public ActionResult Create(IssueEditModel inModel)
		{
			if (ModelState.IsValid)
			{
				var issue = new Issue(_userSession.GetUserName())
				{
					Subject = inModel.Subject,
					Text = inModel.Text
				};
				_repository.Insert(issue);
				return RedirectToAction("Index");
			}
			return View(inModel);
		}

		[HttpPost]
		public ActionResult Close(Guid id)
		{
			var issue = _repository.Get<Issue>(id);
			issue.Open = false;
			_repository.Update(issue);
			return RedirectToAction("Index");
		}

		[HttpPost]
		public ActionResult Delete(Guid id)
		{
			var issue = _repository.Get<Issue>(id);
			_repository.Delete(issue);
			return RedirectToAction("Index");
		}

		public ActionResult Details(Guid id)
		{
			var issue = _repository.Get<Issue>(id);
			return View(issue);
		}
	}
}