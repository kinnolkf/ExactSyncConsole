using CommonLib;
using DropboxAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTransferFactory
{
    public class DownloaderFactory
    {
        public DownloaderFactory(string downloaderName)
        {
            downloaderSwitch = downloaderName;
        }
        string downloaderSwitch;
        public Idownloader GetDownloader()
        {
           
            switch (downloaderSwitch)
            {
                case "Dropbox":
                        return new DropboxDownloader();
                    break;
                default:
                    return null;
            }
        }
    }

    
}
