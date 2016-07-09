using ExactOnline.Client.Sdk.Controllers;
using ExactOnline.Client.Sdk.TestContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExactOnline.Client.Sdk.IntegrationTests
{
	[TestClass]
	public class ExactOnlineClientTests
	{
		[TestCategory("Integration Tests")]
		[TestMethod]
		public void ExactClient_TestEndPointWithSlash_Succeeds()
		{
			var toc = new TestObjectsCreator();
			var client = new ExactOnlineClient(toc.GetWebsite(), toc.GetOAuthAuthenticationToken);
		}

		[TestCategory("Integration Tests")]
		[TestMethod]
		public void ExactClient_TestEndPointWithoutSlash_Succeeds()
		{
			var toc = new TestObjectsCreator();
			var client = new ExactOnlineClient(string.Format("{0}", toc.GetWebsite()), toc.GetOAuthAuthenticationToken);
		}
	}
}
