using ExactOnline.Client.Models;
using ExactOnline.Client.Sdk.Controllers;
using ExactOnline.Client.Sdk.Exceptions;
using ExactOnline.Client.Sdk.TestContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ExactOnline.Client.Sdk.IntegrationTests
{
	[TestClass]
	public class ExactOnlineQueryTests
	{
		[TestMethod]
		[TestCategory("Integration Tests")]
		public void GetSpecificCollectionUsingOData_WithOutOData()
		{
			var toc = new TestObjectsCreator();
			var client = new ExactOnlineClient(toc.EndPoint, toc.GetOAuthAuthenticationToken);

			var accounts = client.For<GLAccount>()
				.Select("Code")
				.Get();
			if (!accounts.Any())
			{
				throw new Exception("The collection of Account entities is empty");
			}
		}

		[TestMethod]
		[TestCategory("Integration Tests")]
		public void GetSpecificCollectionUsingOData_WithWhere()
		{
			var toc = new TestObjectsCreator();
			var client = new ExactOnlineClient(toc.EndPoint, toc.GetOAuthAuthenticationToken);

			var accounts = client.For<GLAccount>()
				.Select("Code")
				.Where("Description+eq+'Gebouwen'")
				.Get();

			if (!accounts.Any())
			{
				throw new Exception("The collection of Account entities is empty");
			}
		}

		[TestMethod]
		[TestCategory("Integration Tests")]
		public void GetSpecificCollectionUsingOData_WithWhereAndAnd()
		{
			var toc = new TestObjectsCreator();
			var client = new ExactOnlineClient(toc.EndPoint, toc.GetOAuthAuthenticationToken);

			var accounts = client.For<GLAccount>()
				.Select("Code")
				.Where("Description+eq+'Gebouwen'")
				.And("Code+eq+'0300'")
				.Get();

			if (!accounts.Any())
			{
				throw new Exception("The collection of Account entities is empty");
			}
		}

		// Is allready being tested in unit tests
		[TestMethod, Ignore]
		[TestCategory("Integration Tests"), ExpectedException(typeof(Exception))]
		public void GetSpecificCollectionUsingOData_WithNonExistingEntity()
		{
			var toc = new TestObjectsCreator();
			var client = new ExactOnlineClient(toc.EndPoint, toc.GetOAuthAuthenticationToken);

			var accounts = client.For<Object>()
				.Where("Description+eq+'Gebouwen'")
				.Get();
		}

		[TestMethod]
		[TestCategory("Integration Tests"), ExpectedException(typeof(BadRequestException))]
		public void GetSpecificCollectionUsingOData_WithNonExistingField()
		{
			var toc = new TestObjectsCreator();
			var client = new ExactOnlineClient(toc.EndPoint, toc.GetOAuthAuthenticationToken);

			var accounts = client.For<Account>()
				.Select("Code")
				.Where("Description+eq+'Gebouwen'")
				.Get();

			if (accounts.Count > 1)
			{
				throw new Exception("The collection has entities, but filter field does not exist. Exception expected.");
			}
		}

		[TestMethod]
		[TestCategory("Integration Tests")]
		public void ExactOnlineQuery_WithCorrectProperty_Succeeds()
		{
			var toc = new TestObjectsCreator();
			var client = new ExactOnlineClient(toc.EndPoint, toc.GetOAuthAuthenticationToken);

			var accounts = client.For<Account>()
				.Select(new[] { "Code" })
				.Get();

			Assert.IsTrue(accounts.Count> 1);
		}

		[TestMethod, ExpectedException(typeof(BadRequestException))]
		[TestCategory("Integration Tests")]
		public void ExactOnlineQuery_WithWrongProperty_Fails()
		{
			var toc = new TestObjectsCreator();
			var client = new ExactOnlineClient(toc.EndPoint, toc.GetOAuthAuthenticationToken);

			client.For<Account>().Select(new[] { "Xxx" }).Get();
		}
	}
}
