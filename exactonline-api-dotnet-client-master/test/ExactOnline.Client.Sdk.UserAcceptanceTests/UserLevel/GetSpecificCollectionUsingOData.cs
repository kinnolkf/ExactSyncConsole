using System.Linq;
using ExactOnline.Client.Models;
using ExactOnline.Client.Sdk.Controllers;
using ExactOnline.Client.Sdk.TestContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExactOnline.Client.Sdk.UserAcceptanceTests.UserLevel
{
	[TestClass]
	public class GetSpecificCollectionCusingOData184244
	{
		[TestMethod]
		[TestCategory("User Acceptance Tests")]
		public void GetSpecificCollectionUsingOData()
		{
			var toc = new TestObjectsCreator();
			var client = new ExactOnlineClient(toc.EndPoint, toc.GetOAuthAuthenticationToken);

			var accounts = client.For<GLAccount>()
				.Select("Code")
				.Where("Description+eq+'Gebouwen'")
				.And("Code+eq+'0300'")
				.Get();

			Assert.IsTrue(accounts.Any());
		}
	
	}
}
