using ExactOnline.Client.Sdk.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExactSyncConsole
{
    public class ExactClientCreator
    {

        public ExactOnlineClient ExactClient()
        {
            // These are the authorisation properties of your app.
            // You can find the values in the App Center when you are maintaining the app.
            const string clientId = "9516ca2b-9deb-4352-8cca-5e68e43c7520";
            const string clientSecret = "etwYOZJeJDxZ";

            // This can be any url as long as it is identical to the callback url you specified for your app in the App Center.
            var callbackUrl = new Uri("https://www.mycompany.com/myapplication");

            var connector = new Connector(clientId, clientSecret, callbackUrl);
            return new ExactOnlineClient(connector.EndPoint, connector.GetAccessToken);

        }
    }
}
