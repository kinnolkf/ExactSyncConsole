using System;
using System.Linq;
using ExactOnline.Client.Models;
using ExactOnline.Client.Sdk.Controllers;
using ExactOnline.Client.Sdk.Exceptions;
using ExactOnline.Client.Sdk.TestContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExactOnline.Client.Sdk.UserAcceptanceTests.UserLevel
{
	[TestClass]
	public class UserDocument
	{
		Guid _documentId;

		[TestCategory("User Acceptance Tests")]
		[TestMethod]
		public void CreateUserDocument()
		{
			var testObject = new TestObjectsCreator();
			var client = new ExactOnlineClient(testObject.EndPoint, testObject.GetOAuthAuthenticationToken);

			var created = CreateDocument(client);
			Assert.IsTrue(created);
		}

		[TestCategory("User Acceptance Tests")]
		[TestMethod]
		public void GetUserDocument()
		{
			var testObject = new TestObjectsCreator();
			var client = new ExactOnlineClient(testObject.EndPoint, testObject.GetOAuthAuthenticationToken);

			var document = GetDocument(client);

			Assert.IsNotNull(document);
			Assert.AreEqual(document.ID, _documentId);
		}

		[TestCategory("User Acceptance Tests")]
		[TestMethod]
		public void UpdateUserDocument()
		{
			var testObject = new TestObjectsCreator();
			var client = new ExactOnlineClient(testObject.EndPoint, testObject.GetOAuthAuthenticationToken);
			var document = GetDocument(client);
			const string subject = "User Acceptance Test Document Updated";
			// Update document
			document.Subject = subject;
			document.DocumentDate = DateTime.Now.Date;
			var updated = client.For<Document>().Update(document);
			var updatedDocument = client.For<Document>().GetEntity(document.ID);

			Assert.IsTrue(updated);
			Assert.IsNotNull(updatedDocument);
			Assert.AreEqual(subject, updatedDocument.Subject);
		}

		[TestCategory("User Acceptance Tests")]
		[TestMethod, ExpectedException(typeof(NotFoundException))]
		public void DeleteUserDocument()
		{
			var testObject = new TestObjectsCreator();
			var client = new ExactOnlineClient(testObject.EndPoint, testObject.GetOAuthAuthenticationToken);

			var document = GetDocument(client);
			var result = client.For<Document>().Delete(document);

			Assert.IsTrue(result);
			// Document does not exist anymore so it throws an exception
			client.For<Document>().GetEntity(document.ID);
		}

		private Document GetDocument(ExactOnlineClient client)
		{
			if (_documentId == Guid.Empty)
			{
				CreateDocument(client);
			}

			var document = client.For<Document>().GetEntity(_documentId);

			return document;
		}

		private static Guid GetCategoryId(ExactOnlineClient client)
		{
			var categories = client.For<DocumentCategory>().Select("ID").Where("Description+eq+'General'").Get();
			var category = categories.First().ID;
			return category;
		}

		private bool CreateDocument(ExactOnlineClient client)
		{
			var document = new Document
			{
				Subject = "User Acceptance Test Document",
				Body = "User Acceptance Test Document",
				Category = GetCategoryId(client),
				Type = 55, //Miscellaneous
				DocumentDate = DateTime.Now.Date
			};

			var created = client.For<Document>().Insert(ref document);
			if (created)
			{
				_documentId = document.ID;
			}

			return created;
		}
	}
}
