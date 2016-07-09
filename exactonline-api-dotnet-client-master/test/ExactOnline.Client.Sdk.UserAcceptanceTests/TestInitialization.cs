using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace User_Acceptance_Tests
{
	[TestClass]
	public class TestInitialization
	{
		[AssemblyInitialize]
		public static void AssemblyInit(TestContext context)
		{
			ServicePointManager.ServerCertificateValidationCallback = TrustAllCertificates;
		}

		[AssemblyCleanup]
		public static void AssemblyCleanup()
		{
		}

		private static bool TrustAllCertificates(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
		{
			return true;
		}
	}
}