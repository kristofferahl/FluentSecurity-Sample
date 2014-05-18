using System;
using System.Linq;
using FluentSecurity.Policy;
using FluentSecurity.TestHelper;
using NUnit.Framework;
using Web;
using Web.App.Security;
using Web.Controllers;
using FluentSecurity.Core;

namespace Specifications
{
	[TestFixture]
	[Category("MvcSecurityConfigurationSpecSpecs")]
	public class When_mvc_security_has_been_bootstrapped
	{
		[Test]
		public void Should_have_expected_setup()
		{
			// Arrange
			var configuration = SecurityConfig.ConfigureSecurity();

			// Act
			var results = configuration.Verify(expectations =>
			{
				expectations.Expect<HomeController>().Has<IgnorePolicy>();

				expectations.Expect<AccountController>(x => x.LogOn()).Has<DenyAuthenticatedAccessPolicy>();
				expectations.Expect<AccountController>(x => x.Register()).Has<DenyAuthenticatedAccessPolicy>();
				expectations.Expect<AccountController>(x => x.LogOff()).Has<DenyAnonymousAccessPolicy>();
				expectations.Expect<AccountController>(x => x.ChangePassword()).Has<DenyAnonymousAccessPolicy>();
				expectations.Expect<AccountController>(x => x.ChangePasswordSuccess()).Has<DenyAnonymousAccessPolicy>();

				expectations.Expect<IssuesController>(x => x.Index()).Has<DenyAnonymousAccessPolicy>();
				expectations.Expect<IssuesController>(x => x.Create()).Has<RequireAnyRolePolicy>();
				expectations.Expect<IssuesController>(x => x.Details(Guid.Empty)).Has<DenyAnonymousAccessPolicy>();
				expectations.Expect<IssuesController>(x => x.Close(Guid.Empty)).Has<RequireAnyRolePolicy>();
				expectations.Expect<IssuesController>(x => x.Delete(Guid.Empty)).Has<RequireAnyRolePolicy>();

				expectations.Expect<UsersController>().Has<RequireAnyRolePolicy>();
				
				expectations.Expect<SetupController>().Has<LocalAccessPolicy>();
			}).ToArray();

			// Assert
			Assert.That(results.Valid(), results.ErrorMessages());
		}

		[Test]
		public void All_actions_should_be_configured()
		{
			// Arrange
			var configuration = SecurityConfig.ConfigureSecurity();

			// Act & assert
			configuration.AssertAllActionsAreConfigured();
		}
	}
}