using ExactOnline.Client.OAuth;
using ExactOnline.Client.Sdk.Helpers;
using ExactOnline.Client.Sdk.Interfaces;
using System;

namespace ExactOnline.Client.Sdk.TestContext
{
	public class TestObjectsCreator
	{
		private const string Website = "https://start.exactonline.nl";

		#region "TODO: Specify these app-specific values"

		/// <summary>
		/// The client id of the app that is used to run the test.
		/// </summary>
		private const string ExactOnlineClientId = "00000000-0000-0000-0000-000000000000";

		/// <summary>
		/// The client secret of the app that is used to run the test.
		/// </summary>
		private const string ExactOnlineClientSecret = "xxxxxxxxxxxx";

		/// <summary>
		/// This uri has to be the same as the callback url of the app.
		/// </summary>
		private readonly Uri _redirectUri = new Uri("https://www.exact.com");

		#endregion

		private readonly static UserAuthorization Authorization = new UserAuthorization();

		private IApiConnector _connector;

		public IApiConnector ApiConnector()
		{
			return _connector ?? (_connector = new ApiConnector(GetOAuthAuthenticationToken));
		}

		public string GetWebsite()
		{
			return Website;
		}

		public string GetOAuthAuthenticationToken()
		{
			UserAuthorizations.Authorize(Authorization, EndPoint, ExactOnlineClientId, ExactOnlineClientSecret, _redirectUri);

			return Authorization.AccessToken;
		}

		public string EndPoint
		{
			get
			{
				return Website;
			}
		}

		public string UriGlAccount(int currentDivision)
		{
			return String.Format("{0}/api/v1/{1}/financial/GLAccounts", Website, currentDivision);
		}

		public string UriCrmAccount(int currentDivision)
		{
			return String.Format("{0}/api/v1/{1}/crm/Accounts", Website, currentDivision);
		}


		public int GetCurrentDivision()
		{

			var currentDivision = -1;

			if (_connector == null)
			{
				ApiConnector();
			}

			if (_connector != null)
			{
				currentDivision = _connector.GetCurrentDivision(Website);
			}

			return currentDivision;
		}
	}
}
