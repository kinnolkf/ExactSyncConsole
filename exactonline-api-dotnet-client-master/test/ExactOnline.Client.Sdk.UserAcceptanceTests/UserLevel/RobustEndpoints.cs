using ExactOnline.Client.Models;
using ExactOnline.Client.Sdk.Controllers;
using ExactOnline.Client.Sdk.TestContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExactOnline.Client.Sdk.UserAcceptanceTests.UserLevel
{
	[TestClass]
	public class RobustEndpoints
	{
		private int _currentDivision;
		private TestObjectsCreator _toc;

		[TestInitialize]
		public void InitializeSharedTestObjects()
		{
			_toc = new TestObjectsCreator();
			_currentDivision = GetCurrentDivision(_toc);
		}

		private static int GetCurrentDivision(TestObjectsCreator toc)
		{
			var clientWithoutDivision = new ExactOnlineClient(toc.GetWebsite(), toc.GetOAuthAuthenticationToken);
			return clientWithoutDivision.GetDivision();
		}

		[TestMethod]
		[TestCategory("User Acceptance Tests")]
		public void TestWithoutDivision()
		{
			var client = new ExactOnlineClient(_toc.GetWebsite(), _toc.GetOAuthAuthenticationToken);
			client.For<Account>().Select("Code").Get();
		}

		[TestMethod]
		[TestCategory("User Acceptance Tests")]
		public void TestWithDivision()
		{
			var client = new ExactOnlineClient(_toc.GetWebsite(), _currentDivision, _toc.GetOAuthAuthenticationToken);
			client.For<Account>().Select("Code").Get();
		}

		[TestMethod]
		[TestCategory("User Acceptance Tests")]
		public void TestWithSlash()
		{
			var client = new ExactOnlineClient(_toc.GetWebsite(), _currentDivision, _toc.GetOAuthAuthenticationToken);
			client.For<Account>().Select("Code").Get();
		}

	}
}
