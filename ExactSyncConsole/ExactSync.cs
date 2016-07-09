using CommonLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileTransferFactory;
using System.Threading;

namespace ExactSyncConsole
{
    public class ExactSync
    {
        private DateTime lastSyncDateTime = DateTime.MinValue;
        
        private string downloadPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)
            , "ExactSync", "test.xml");

        private Idownloader downloader;
        private Iuploader uploader;

        private DownloaderFactory downloadFactory;
        private UploaderFactory uploadFactory;

        public ExactSync()
        {
            Initialize();
        }

        private void Initialize()
        {
            downloadFactory = new FileTransferFactory.DownloaderFactory("Dropbox");

            uploadFactory = new FileTransferFactory.UploaderFactory("ExactOnline");
        }

        public void DownloaderDoWork()
        {
            GetDownloader();

            if (downloader != null)
            {
                var fileList = downloader.GetFilesList();
                var latestFilelist = fileList.Where(x => x.Value < DateTime.Now.AddHours(-8)
                    && x.Value > lastSyncDateTime);
                if (latestFilelist?.Count() > 0)
                {

                    (new FileInfo(downloadPath)).Directory.Create();
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

        public void UploaderDoWork()
        {
            GetUploader();
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

        private void GetDownloader()
        {
            try
            {
                var tokenSource = new CancellationTokenSource();
                CancellationToken token = tokenSource.Token;
                int timeOut = 60000;

                var task =Task.Factory.StartNew(x
                    =>
                {
                    downloader= downloadFactory.GetDownloader();
                }, token);

                if (!task.Wait(timeOut, token))
                    throw new TimeoutException("Get Downloader timeout");
            }
            catch (Exception ex)
            {
                if (downloader == null)
                    Console.WriteLine("downloader Offline with exception :" + ex.ToString());
            }
        }

        private void GetUploader()
        {
            try
            {
                uploader = uploadFactory.GetUploader();

                //var tokenSource = new CancellationTokenSource();
                //CancellationToken token = tokenSource.Token;
                //int timeOut = 60000;

                //var task = Task.Factory.StartNew(x
                //     =>
                //{
                //    uploader = uploadFactory.GetUploader();
                //}, token);

                //if (!task.Wait(timeOut, token))
                //    throw new TimeoutException("Get Uploader timeout");
            }
            catch (Exception ex)
            {
                var excep = ex.ToString();
                if (uploader == null)
                    Console.WriteLine("uploader Offline with exception :" + ex.ToString());
            }
        }
        
    }
}
