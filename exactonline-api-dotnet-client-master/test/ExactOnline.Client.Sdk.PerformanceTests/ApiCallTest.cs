using System;
using System.Linq;
using ExactOnline.Client.Models;
using ExactOnline.Client.Sdk.Controllers;
using ExactOnline.Client.Sdk.Interfaces;
using ExactOnline.Client.Sdk.PerformanceTests.Helpers;
using ExactOnline.Client.Sdk.TestContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExactOnline.Client.Sdk.PerformanceTests
{
	/// <summary>
	/// Summary description for APICalls
	/// </summary>
	[TestClass]
	public class ApiCallTest
	{
		TestObjectsCreator _toc;
		IApiConnector _conn;
		private int _currentDivision;

		[TestInitialize()]
		public void Setup()
		{
			_toc = new TestObjectsCreator();
			_currentDivision = _toc.GetCurrentDivision();
			_conn = _toc.ApiConnector();
		}

		[TestCategory("Performance Test")]
		[TestMethod]
		public void TestPerformanceApiCallGet()
		{
			var originalprocesstime = TimeSpan.FromSeconds(5.5);
			var currentprocesstime = TestTimer.Time(DoGetRequest);
			Assert.IsTrue(currentprocesstime < originalprocesstime);
		}

		[TestCategory("Performance Test")]
		[TestMethod]
		public void TestPerformanceApiCallPost()
		{
			var originalprocesstime = TimeSpan.FromSeconds(5.8);
			var currentprocesstime = TestTimer.Time(DoPostRequest);
			Assert.IsTrue(currentprocesstime < originalprocesstime);
		}

		[TestCategory("Performance Test")]
		[TestMethod]
		public void TestPerformanceApiCallPut()
		{
			var client = new ExactOnlineClient(_toc.EndPoint, _toc.GetOAuthAuthenticationToken);
			var account = client.For<Account>().Select("ID").Where("Name+eq+'43905139517985179437'").Get().First();

			var originalprocesstime = TimeSpan.FromSeconds(11.8);
			var currentprocesstime = TestTimer.Time(() => DoPutRequest(account));
			Assert.IsTrue(currentprocesstime < originalprocesstime);
		}

		[TestCategory("Performance Test")]
		[TestMethod]
		public void TestPerformanceApiCallDelete()
		{
			var client = new ExactOnlineClient(_toc.EndPoint, _toc.GetOAuthAuthenticationToken);
			var account = client.For<Account>().Select("ID").Where("Name+eq+'43905139517985179437'").Get().FirstOrDefault();
		
			var originalprocesstime = TimeSpan.FromSeconds(13.0);
			var currentprocesstime = TestTimer.Time(() => DoDeleteRequest(account));
			Assert.IsTrue(currentprocesstime < originalprocesstime);
		}

		private void DoGetRequest()
		{
			for (int i = 0; i < 10; i++)
			{
				_conn.DoGetRequest(_toc.UriGlAccount(_currentDivision), string.Empty);
			}
		}

		private void DoPostRequest()
		{
			for (int i = 0; i < 10; i++)
			{
				_conn.DoPostRequest(_toc.UriCrmAccount(_currentDivision), "{\"Name\": \"43905139517985179437\"}");
			}
		}

		private void DoPutRequest(Account account)
		{
			for (int i = 0; i < 10; i++)
			{
				_conn.DoPutRequest(_toc.UriCrmAccount(_currentDivision) + "(guid'" + account.ID.ToString() + "')", "{\"Name\": \"43905139517985179437\"}");
			}
		}

		private void DoDeleteRequest(Account account)
		{
			string endpoint = _toc.UriCrmAccount(_currentDivision) + "(guid'" + account.ID.ToString() + "')";
			_conn.DoDeleteRequest(endpoint);
		}

	}
}
