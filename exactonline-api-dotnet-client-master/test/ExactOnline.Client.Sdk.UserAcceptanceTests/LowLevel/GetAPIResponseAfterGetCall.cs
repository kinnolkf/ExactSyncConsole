﻿using System;
using ExactOnline.Client.Sdk.Helpers;
using ExactOnline.Client.Sdk.TestContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace ExactOnline.Client.Sdk.UserAcceptanceTests.LowLevel
{
	[TestClass]
	public class GetApiResponseAfterGetCall
	{
		/// <summary>
		/// User Story: Get a text response in JSON format from the API after 
		/// executing a REST GET call so that I can read data from Exact Online.
		/// Constraints: The user retrieves a string in JSON format.
		/// </summary>
		[TestMethod]
		[TestCategory("User Acceptance Tests")]
		public void GetApiResponseAfterGetCall_Succeeds()
		{
			//APIConnector connector = new APIConnector(accesstoken);
			var toc = new TestObjectsCreator();
			var conn = new ApiConnection(toc.ApiConnector(), toc.UriCrmAccount(toc.GetCurrentDivision()));

			string result = conn.Get(string.Empty);
			if (result == string.Empty)
			{
				throw new Exception("Return from API was empty");
			}
			else
			{
				// Check if the response is a JSON Value
				// Throws exception of not JSON
				JsonConvert.DeserializeObject(result);
			}
		}
	}
}
