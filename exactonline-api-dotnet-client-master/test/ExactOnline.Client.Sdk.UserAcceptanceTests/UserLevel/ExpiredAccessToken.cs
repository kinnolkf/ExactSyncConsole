using ExactOnline.Client.Models;
using ExactOnline.Client.Sdk.Controllers;
using ExactOnline.Client.Sdk.TestContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExactOnline.Client.Sdk.UserAcceptanceTests.UserLevel
{
	[TestClass]
	public class ExpiredAccessToken
	{
		// Run this test selectively because it takes 10 minutes
		[TestMethod]
		[TestCategory("User Acceptance Tests")]
		[Ignore]
		public void ExpiredAccessToken_Succeeds()
		{
			var toc = new TestObjectsCreator();
			var client = new ExactOnlineClient(toc.EndPoint, toc.GetOAuthAuthenticationToken);

			System.Threading.Thread.Sleep(600000); //Sleep for 10 minutes, then the token is expired

			var accounts = client.For<Account>().Select("ID").Get(); 
			Assert.IsTrue(accounts.Count > 0);
		}
	}
}
