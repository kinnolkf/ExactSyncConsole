using CommonLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExactSyncConsole
{
    class Program
    {
        private static DateTime lastSyncDateTime = DateTime.MinValue;
        const int syncFrequencyMinutes = 1;
        private static string downloadPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)
            , "ExactSync","test.xml");

        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                FileTransferFactory.DownloaderFactory downloadFactory = new FileTransferFactory.DownloaderFactory("Dropbox");

                FileTransferFactory.UploaderFactory uploadFactory = new FileTransferFactory.UploaderFactory("ExactOnline");
                Idownloader downloader = null;
                Iuploader uploader = null;
                try
                {
                    downloader = downloadFactory.GetDownloader();
                }
                catch (Exception ex)
                {
                    if (downloader == null)
                        Console.WriteLine("downloader Offline with exception :" + ex.ToString());
                }

                try
                {
                    uploader = uploadFactory.GetUploader();
                }
                catch (Exception ex)
                {
                    if (uploader == null)
                        Console.WriteLine("uploader Offline with exception :" + ex.ToString());
                }

                while (true)
                {
                    DownloaderDoWork(downloader);

                    UploaderDoWork(uploader);

                    Thread.Sleep(syncFrequencyMinutes * 60 * 1000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to Sync with exception :" + ex.GetBaseException().ToString());

                Console.ReadKey();
            }



        }

        static void DownloaderDoWork(Idownloader downloader)
        {
            if (downloader != null)
            {
                var fileList = downloader.GetFilesList();
                var latestFilelist = fileList.Where(x => x.Value < DateTime.Now.AddHours(-8)
                    && x.Value > lastSyncDateTime);
                if (latestFilelist?.Count() > 0)
                {
                   
                    (new FileInfo(downloadPath)).Directory.Create();
                    var test = latestFilelist.ToList();
                    foreach (var file in latestFilelist.ToList())
                    {
                        if (downloader.DownloadFile(file.Key, (new FileInfo(downloadPath)).Directory.ToString()))
                            downloader.UpdateDownloadedFile(file.Key, DateTime.Now.AddHours(-8));
                    }
                    lastSyncDateTime = DateTime.Now.AddHours(-8);
                    //this datetime is in GMT +0 , as dropbox file modified date are store at gmt+0

                }
            }
        }

        static void UploaderDoWork(Iuploader uploader)
        {
            if (uploader != null)
            {
                string[] files = Directory.GetFiles(downloadPath);
                string destPath = Path.Combine(downloadPath, "processed");
                if (Directory.Exists(destPath) == false)
                    Directory.CreateDirectory(destPath);

                var filesToUpload = Directory.GetFiles(downloadPath);

                foreach (var file in filesToUpload)
                {
                    if (uploader.UploadFile(file))
                    {
                        //this datetime is in GMT +0 , as dropbox file modified date are store at gmt+0
                        uploader.UpdateUploadedFile(file, DateTime.Now.AddHours(-8));
                        //Move file / delete file
                        File.Move(file, Path.Combine(destPath, Path.GetFileName(file)));
                    }
                }
            }
        }
    }
}
