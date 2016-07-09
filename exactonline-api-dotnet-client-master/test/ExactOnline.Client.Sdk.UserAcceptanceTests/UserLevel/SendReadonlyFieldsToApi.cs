﻿using System;
using System.Linq;
using ExactOnline.Client.Models;
using ExactOnline.Client.Sdk.Controllers;
using ExactOnline.Client.Sdk.TestContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExactOnline.Client.Sdk.UserAcceptanceTests.UserLevel
{
	[TestClass]
	public class SendReadonlyFieldsToApiTest
	{
		[TestMethod]
		[TestCategory("User Acceptance Tests")]
		public void UpdateReadonlyFields_IgnoresReadonlyFields()
		{
			var toc = new TestObjectsCreator();
			var client = new ExactOnlineClient(toc.EndPoint, toc.GetOAuthAuthenticationToken);

			var account = client.For<Account>().Top(1).Select("ID,Name").Get().First();
			var originalName = account.Name;
			var originalId = account.ID;

			account.Name = "Test account name2";
			account.Created = DateTime.Now;
			account.Creator = new Guid("c20a5590-c605-4f59-8fbb-112ee142bc59");
			account.CreatorFullName = "Edward Jackson";
			account.ID = originalId;
			account.LogoThumbnailUrl = "www.google.nl";
			account.Modified = DateTime.Now;
			account.Modifier = new Guid("c20a5590-c605-4f59-8fbb-112ee142bc59");
			account.ModifierFullName = "Test";
			account.ClassificationDescription = "Test";
			account.CostcenterDescription = "Test";
			account.InvoiceAccountCode = "Test";
			account.InvoiceAccountName = "Test";
			account.LanguageDescription = "Bla";
			account.PurchaseCurrencyDescription = "Test";
			account.ResellerCode = "Test";
			account.ResellerName = "Test";
			account.SalesCurrencyDescription = "Test";
			account.SalesVATCodeDescription = "Test";
			account.StateName = "Test";
			Assert.IsTrue(client.For<Account>().Update(account));

			// Change it back to testname 
			account.Name = originalName;
			Assert.IsTrue(client.For<Account>().Update(account));
		}
	}
}
