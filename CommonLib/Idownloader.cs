using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public interface Idownloader
    {
        /// <summary>
        /// Get file and upload time in key pair value. 
        /// </summary>
        /// <returns></returns>
        Dictionary<string, DateTime> GetFilesList();

        bool DownloadFile(string fileName, string destinationPath);

        Dictionary<string, DateTime> UpdateDownloadedFile(string fileName, DateTime downloadedDate);

    }
}
