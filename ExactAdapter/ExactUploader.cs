using CommonLib;
using ExactOnline.Client.Models;
using ExactOnline.Client.Sdk.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExactAdapter
{
    public class ExactUploader : Iuploader
    {
        private Guid _documentId;

        public ExactUploader()
        {
            InitializeUploader();
            //Client = client as ExactOnlineClient;
        }
        private ExactOnlineClient _client;
        private void InitializeUploader()
        {
            // These are the authorisation properties of your app.
            // You can find the values in the App Center when you are maintaining the app.
            const string clientId = "9516ca2b-9deb-4352-8cca-5e68e43c7520";
            const string clientSecret = "etwYOZJeJDxZ";

            // This can be any url as long as it is identical to the callback url you specified for your app in the App Center.
            var callbackUrl = new Uri("https://www.mycompany.com/myapplication");

            var connector = new Connector(clientId, clientSecret, callbackUrl);
            _client = new ExactOnlineClient(connector.EndPoint, connector.GetAccessToken);

            // Get the Code and Name of a random account in the administration
            var fields = new[] { "Code", "Name" };
            var account = _client.For<Account>().Top(1).Select(fields).Get().FirstOrDefault();

            Console.WriteLine("Account {0} - {1}", account.Code.TrimStart(), account.Name);
        }

        public bool UploadFile(string fullFileName)
        {
            try
            {
                Encoding isWhichEncoding = Encoding.UTF8;
                //check if it is text
                if (IsText(out isWhichEncoding, fullFileName, 1024))
                {
                    StringBuilder sb = new StringBuilder();
                    string[] lines = File.ReadAllLines(fullFileName);

                    foreach (var line in lines)
                    {
                        sb.Append(line).AppendLine();
                    }

                    var document = new Document
                    {
                        Subject = Path.GetFileName(fullFileName),
                        Body = sb.ToString(),
                        Category = GetCategoryId(_client),
                        Type = 55, //Miscellaneous
                        DocumentDate = DateTime.Now.Date
                    };

                    var created = _client.For<Document>().Insert(ref document);
                    if (created)
                    {
                        _documentId = document.ID;
                        Console.WriteLine("Document Inserted");
                    }
                    return created;
                }
                //not text, unable to write to document
                return false;
            }
            catch (Exception)
            {

                throw;
            }

            
        }

        private Guid GetCategoryId(ExactOnlineClient client)
        {
            var categories = client.For<DocumentCategory>().Select("ID").Where("Description+eq+'General'").Get();
            var category = categories.First().ID;
            return category;
        }

        private bool IsText(out Encoding encoding, string fileName, int windowSize)
        {
            using (var fileStream = File.OpenRead(fileName))
            {
                var rawData = new byte[windowSize];
                var text = new char[windowSize];
                var isText = true;

                // Read raw bytes
                var rawLength = fileStream.Read(rawData, 0, rawData.Length);
                fileStream.Seek(0, SeekOrigin.Begin);

                if (rawData[0] == 0xef && rawData[1] == 0xbb && rawData[2] == 0xbf)
                {
                    encoding = Encoding.UTF8;
                }
                else if (rawData[0] == 0xfe && rawData[1] == 0xff)
                {
                    encoding = Encoding.Unicode;
                }
                else if (rawData[0] == 0 && rawData[1] == 0 && rawData[2] == 0xfe && rawData[3] == 0xff)
                {
                    encoding = Encoding.UTF32;
                }
                else if (rawData[0] == 0x2b && rawData[1] == 0x2f && rawData[2] == 0x76)
                {
                    encoding = Encoding.UTF7;
                }
                else
                {
                    encoding = Encoding.Default;
                }

                // Read text and detect the encoding
                using (var streamReader = new StreamReader(fileStream))
                {
                    streamReader.Read(text, 0, text.Length);
                }

                using (var memoryStream = new MemoryStream())
                {
                    using (var streamWriter = new StreamWriter(memoryStream, encoding))
                    {
                        // Write the text to a buffer
                        streamWriter.Write(text);
                        streamWriter.Flush();

                        // Get the buffer from the memory stream for comparision
                        var memoryBuffer = memoryStream.GetBuffer();

                        // Compare only bytes read
                        for (var i = 0; i < rawLength && isText; i++)
                        {
                            isText = rawData[i] == memoryBuffer[i];
                        }
                    }
                }

                return isText;
            }
        }

        public Dictionary<string, DateTime> UpdateUploadedFile(string fileName, DateTime uploadedDate)
        {
            if (!_uploadedFiles.ContainsKey(fileName))
                _uploadedFiles.Add(fileName, uploadedDate);
            else
                _uploadedFiles[fileName] = uploadedDate;

            return _uploadedFiles;
        }

        private Dictionary<string, DateTime> _uploadedFiles = new Dictionary<string, DateTime>();
    }
}
