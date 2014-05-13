using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Core.Domain;
using Core.Domain.Persistence;
using Api.Models;
using Core.Services;

namespace Api.Controllers
{
	public class IssuesController : ApiController
	{
		private readonly IDomainRepository _domainRepository;
		private readonly IUserSession _userSession;

		public IssuesController(IDomainRepository domainRepository, IUserSession userSession)
		{
			_domainRepository = domainRepository;
			_userSession = userSession;
		}

		private static IssueModels.Details MapIssueToDetailsModel(Issue issue)
		{
			return new IssueModels.Details
				{
					Id = issue.Id,
					Subject = issue.Subject,
					Text = issue.Text,
					Submitted = issue.Submitted,
					Modified = issue.Modified,
					Open = issue.Open,
					User = issue.User
				};
		}

		public IEnumerable<IssueModels.Details> Get()
		{
			var issues = _domainRepository.GetAll<Issue>();
			return issues.Select(MapIssueToDetailsModel);
		}

		public IssueModels.Details Get(Guid id)
		{
			var issue = _domainRepository.Get<Issue>(id);
			if (issue == null) throw new HttpResponseException(HttpStatusCode.NotFound);

			return MapIssueToDetailsModel(issue);
		}

		public Guid Post([FromBody] IssueModels.Post model)
		{
			var issue = new Issue(_userSession.GetUserName())
				{
					Subject = model.Subject,
					Text = model.Text,
					Submitted = model.Submitted
				};
			_domainRepository.Insert(issue);

			return issue.Id;
		}

		public void Put(Guid id, [FromBody]IssueModels.Put model)
		{
			var issue = _domainRepository.Get<Issue>(id);
			if (issue == null) throw new HttpResponseException(HttpStatusCode.NotFound);

			issue.Subject = model.Subject;
			issue.Text = model.Subject;
			issue.Modified = model.Modified;

			_domainRepository.Update(issue);
		}

		public void Delete(Guid id)
		{
			var issue = _domainRepository.Get<Issue>(id);
			_domainRepository.Delete(issue);
		}
	}
}
