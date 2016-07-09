using System;
using System.Collections.Generic;
using ExactOnline.Client.Sdk.Controllers;
using ExactOnline.Client.Sdk.Helpers;
using ExactOnline.Client.Sdk.TestContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExactOnline.Client.Sdk.UserAcceptanceTests.LowLevel
{
	[TestClass]
	public class GetCollectionOfAccountsInJsonFormat
	{
		[TestCategory("User Acceptance Tests")]
		[TestMethod]
		public void GetCollectionOfAccountsInJsonFormat_Succeeds()
		{
			var toc = new TestObjectsCreator();
			var conn = new ApiConnection(toc.ApiConnector(), toc.UriCrmAccount(toc.GetCurrentDivision()));

			var c = new SimpleController(conn);
			List<dynamic> accounts = c.GetDynamic(string.Empty);

			// Test if list has entities (easy test, because actual entity count isn't known)
			if (accounts.Count < 1)
			{
				throw new Exception("User Story not correctly implemented: List of CRM/Accounts is empty");
			}
		}
	}
}
