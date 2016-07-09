using ExactOnline.Client.Models;
using ExactOnline.Client.Sdk.Controllers;
using ExactOnline.Client.Sdk.TestContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExactOnline.Client.Sdk.UserAcceptanceTests.UserLevel
{
	[TestClass]
	public class GetCollectionOfAllAcountEntitiesInCSharpObjects
	{
		[TestMethod]
		[TestCategory("User Acceptance Tests")]
		public void GetCollectionOfAllAcountEntitiesInCSharpObjects_Succeeds()
		{
			var toc = new TestObjectsCreator();

			var client = new ExactOnlineClient(toc.EndPoint, toc.GetOAuthAuthenticationToken);
			var accounts = client.For<Account>().Select("ID").Get();
			Assert.IsTrue(accounts.Count > 0, "Get Collection Of All Account Entities in CSharp Objects is not implemented corectly");

		}
	}
}
