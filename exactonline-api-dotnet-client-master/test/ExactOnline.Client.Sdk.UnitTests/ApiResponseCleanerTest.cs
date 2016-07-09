﻿using ExactOnline.Client.Sdk.Exceptions;
using ExactOnline.Client.Sdk.Helpers;
using ExactOnline.Client.Sdk.UnitTests.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ExactOnline.Client.Sdk.UnitTests
{
	[TestClass]
	public class ApiResponseCleanerTest
	{

		#region Additional Test Attributes

		public TestContext TestContext { get; set; }

		#endregion

		#region Test: Fetch Json Array
		[TestCategory("Unit Test")]
		[TestMethod]
		public void ApiResponseCleaner_FetchJsonArray_WithCorrectValues_Succeeds()
		{
			string jsonarray = ApiResponseCleaner.GetJsonArray(JsonFileReader.GetJsonFromFile("ApiResponse_Json_Array.txt"));
		}

		[TestCategory("Unit Test")]
		[TestMethod, ExpectedException(typeof(IncorrectJsonException))]
		public void ApiResponseCleaner_FetchJsonArray_WithOutDKeyValuePair_Fails()
		{
			ApiResponseCleaner.GetJsonArray(JsonFileReader.GetJsonFromFile("ApiResponse_Json_Array_WithoutDTag.txt"));
		}

		[TestCategory("Unit Test")]
		[TestMethod, ExpectedException(typeof(IncorrectJsonException))]
		public void ApiResponseCleaner_FetchJsonArray_WithOutResultsKeyValuePair_Fails()
		{
			ApiResponseCleaner.GetJsonArray(JsonFileReader.GetJsonFromFile("ApiResponse_Json_Array_WithoutResultsTag.txt"));
		}

		[TestCategory("Unit Test")]
		[TestMethod]
		public void ApiResponseCleanerFetchJsonArrayWithEmptyLinkedEntitiesSucceeds()
		{
			const string expected = @"[{""BankAccounts"":[]}]";
			string clean = ApiResponseCleaner.GetJsonArray(JsonFileReader.GetJsonFromFile("ApiResponse_Json_Array_WithEmptyLinkedEntities.txt"));
			Assert.AreEqual(expected, clean);
		}
		#endregion

		#region Test: Fetch Json Object
		[TestCategory("Unit Test")]
		[TestMethod]
		public void ApiResponseCleaner_FetchJsonObject_WithCorrectValues_Succeeds()
		{
			ApiResponseCleaner.GetJsonObject(JsonFileReader.GetJsonFromFile("ApiResponse_Json_Object.txt"));
		}

		[TestCategory("Unit Test")]
		[TestMethod]
		public void ApiResponseCleaner_FetchJsonObject_WithEscapeCharacter_Succeeds()
		{
			const string sampleJsonResponse = @"{ ""d"": { ""Remarks"": ""\\escape test"" }}";			

			var cleanedJson = ApiResponseCleaner.GetJsonObject(sampleJsonResponse);

			const string expectedCleanedJson = @"{""Remarks"":""\\escape test""}";
			Assert.AreEqual(expectedCleanedJson, cleanedJson);
		}

		[TestCategory("Unit Test")]
		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void ApiResponseCleaner_FetchJsonObject_WithoutDKeyValuePair_Fails()
		{
			ApiResponseCleaner.GetJsonObject(JsonFileReader.GetJsonFromFile("ApiResponse_Json_Object_WithoutD.txt"));
		}
		#endregion
	}

}
