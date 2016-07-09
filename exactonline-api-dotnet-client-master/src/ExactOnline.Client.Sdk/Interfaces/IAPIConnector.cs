namespace ExactOnline.Client.Sdk.Interfaces
{
	public interface IApiConnector
	{
		string DoGetRequest(string endpoint, string parameters);

		string DoPostRequest(string endpoint, string postdata);

		string DoPutRequest(string endpoint, string putData);

		string DoDeleteRequest(string endpoint);

		string DoCleanRequest(string uri); // Request without Content-Type for $count function

		int GetCurrentDivision(string website);
	}
}
