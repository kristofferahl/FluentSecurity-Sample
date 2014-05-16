using System.Web;
using FluentSecurity;
using FluentSecurity.Policy;

namespace Web.App.Security
{
	public class LocalAccessPolicy : ISecurityPolicy
	{
		public PolicyResult Enforce(ISecurityContext context)
		{
			return HttpContext.Current.Request.IsLocal
				? PolicyResult.CreateSuccessResult(this)
				: PolicyResult.CreateFailureResult(this, "Access restricted to local access only");
		}
	}
}