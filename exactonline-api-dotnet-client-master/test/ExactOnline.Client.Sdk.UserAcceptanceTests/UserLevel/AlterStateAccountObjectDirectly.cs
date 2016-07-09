﻿using System;
using System.Linq;
using ExactOnline.Client.Models;
using ExactOnline.Client.Sdk.Controllers;
using ExactOnline.Client.Sdk.TestContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExactOnline.Client.Sdk.UserAcceptanceTests.UserLevel
{	
	[TestClass]
	public class AlterStateAccountObjectDirectly
	{
		[TestMethod]
		[TestCategory("User Acceptance Tests")]
		public void AlterStateAccountObjectDirectly_Succeeds()
		{
			var toc = new TestObjectsCreator();
			var client = new ExactOnlineClient(toc.EndPoint, toc.GetOAuthAuthenticationToken);
			// Get account to update
			var accounts = client.For<Account>().Top(1).Select("ID").Get();
			var account = accounts.First();

			// Change name of account & update
			account.Name = "Test Name UAT2";
			if (!client.For<Account>().Update(account))
			{
				throw new Exception("Account is not updated");
			}
			else
			{
				// Change account back for test purposes
				account.Name = "Test Name UAT";
				if (!client.For<Account>().Update(account))
				{
					throw new Exception("Changing account entity back for tests failed. Possibly these tests won't work anymore");
				}
			}
		}
	}
}
