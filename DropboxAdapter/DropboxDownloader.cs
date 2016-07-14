using CommonLib;
using Dropbox.Api;
using Dropbox.Api.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DropboxAdapter
{
    public class DropboxDownloader : Idownloader
    {
        private const string ApiKey = "br26fgdvj88fqtw";
        public DropboxDownloader()
        {
            InitializeDownloader();
        }
        private string path = "/DotNetApi/Help";
        private async void InitializeDownloader()
        {
                DropboxCertHelper.InitializeCertPinning();

                var accessToken = "";
                if (string.IsNullOrEmpty(accessToken))
                {
                Console.WriteLine("Unable to get AcessToken from " + "DropboxDownloader");
                }

                // Specify socket level timeout which decides maximum waiting time when on bytes are
                // received by the socket.
                var httpClient = new HttpClient(new WebRequestHandler { ReadWriteTimeout = 10 * 1000 })
                {
                    // Specify request level timeout which decides maximum time taht can be spent on
                    // download/upload files.
                    Timeout = TimeSpan.FromMinutes(20)
                };

                try
                {
                    var config = new DropboxClientConfig("ExactFileSync")
                    {
                        HttpClient = httpClient
                    };

                currentClient = new DropboxClient(accessToken, config);

                    // Tests below are for Dropbox Business endpoints. To run these tests, make sure the ApiKey is for
                    // a Dropbox Business app and you have an admin account to log in.

                    /*
                    var client = new DropboxTeamClient(accessToken, userAgent: "SimpleTeamTestApp", httpClient: httpClient);
                    await RunTeamTests(client);
                    */
                }
                catch (HttpException e)
                {
                    Console.WriteLine("Exception reported from RPC layer");
                    Console.WriteLine("    Status code: {0}", e.StatusCode);
                    Console.WriteLine("    Message    : {0}", e.Message);
                    if (e.RequestUri != null)
                    {
                        Console.WriteLine("    Request uri: {0}", e.RequestUri);
                    }
                }

            


        }
        DropboxClient currentClient;
        public async Task<bool> Download(string fileName, string destinationPath)
        {
            var list = await ListFolder(currentClient);

            var files = list.Entries.FirstOrDefault(i => i.IsFile && i.Name == fileName);
            if (files != null)
            {
                await excecuteDownloadFile(currentClient, path, files.AsFile,destinationPath);
            }

            return true;
        }

        public Dictionary<string, DateTime> GetFilesList()
        {
            var t = ListFolder(currentClient);

            var result = t.Result;

            return result.Entries.Select(x => new { x.Name, x.AsFile.ServerModified })
                   .ToDictionary(x => x.Name, x => x.ServerModified);
        }

        private Dictionary<string, DateTime> fileList;

        private async Task excecuteDownloadFile(DropboxClient client, string folder, FileMetadata file,string destinationPath)
        {
            Console.WriteLine("Download file...");

            using (var response = await client.Files.DownloadAsync(folder + "/" + file.Name))
            {
                Console.WriteLine("Downloaded {0} Rev {1}", response.Response.Name, response.Response.Rev);
                Console.WriteLine("------------------------------");
                Console.WriteLine(await response.GetContentAsStringAsync());
                Console.WriteLine("------------------------------");

                var x = await response.GetContentAsStreamAsync();
                using (Stream responseStream = await response.GetContentAsStreamAsync())
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead;
                    do
                    {
                        bytesRead = responseStream.Read(buffer, 0, buffer.Length);
                        memoryStream.Write(buffer, 0, bytesRead);
                    } while (bytesRead > 0);

                    using (FileStream outFile = new FileStream(Path.Combine(destinationPath, file.Name)
                        , FileMode.Create, System.IO.FileAccess.Write))
                    {
                        memoryStream.Position = 0;
                        memoryStream.CopyTo(outFile);
                    }
                }
            }
        }

        private async Task GetCurrentAccount(DropboxClient client)
        {
            var full = await client.Users.GetCurrentAccountAsync();

            Console.WriteLine("Account id    : {0}", full.AccountId);
            Console.WriteLine("Country       : {0}", full.Country);
            Console.WriteLine("Email         : {0}", full.Email);
            Console.WriteLine("Is paired     : {0}", full.IsPaired ? "Yes" : "No");
            Console.WriteLine("Locale        : {0}", full.Locale);
            Console.WriteLine("Name");
            Console.WriteLine("  Display  : {0}", full.Name.DisplayName);
            Console.WriteLine("  Familiar : {0}", full.Name.FamiliarName);
            Console.WriteLine("  Given    : {0}", full.Name.GivenName);
            Console.WriteLine("  Surname  : {0}", full.Name.Surname);
            Console.WriteLine("Referral link : {0}", full.ReferralLink);

            if (full.Team != null)
            {
                Console.WriteLine("Team");
                Console.WriteLine("  Id   : {0}", full.Team.Id);
                Console.WriteLine("  Name : {0}", full.Team.Name);
            }
            else
            {
                Console.WriteLine("Team - None");
            }
        }
        private async Task<ListFolderResult> ListFolder(DropboxClient client)
        {
            Console.WriteLine("--- Files ---");
            var list = await client.Files.ListFolderAsync(path);

            // show folders then files
            foreach (var item in list.Entries.Where(i => i.IsFolder))
            {
                Console.WriteLine("D  {0}/", item.Name);
            }
            fileList = new Dictionary<string, DateTime>();
            foreach (var item in list.Entries.Where(i => i.IsFile))
            {
                var file = item.AsFile;

                fileList.Add(file.Name, file.ServerModified);
            }

            if (list.HasMore)
            {
                Console.WriteLine("   ...");
            }
            return list;
        }

        public bool DownloadFile(string fileName, string destinationPath)
        {
            var x = Download(fileName, destinationPath);

            return x.Result;
        }

        public Dictionary<string, DateTime> UpdateDownloadedFile(string fileName, DateTime downloadedDate)
        {
            if (!_downloadedFiles.ContainsKey(fileName))
                _downloadedFiles.Add(fileName, downloadedDate);
            else
                _downloadedFiles[fileName] = downloadedDate;

            return _downloadedFiles;
        }

        private Dictionary<string, DateTime> _downloadedFiles = new Dictionary<string, DateTime>();
    }
}
