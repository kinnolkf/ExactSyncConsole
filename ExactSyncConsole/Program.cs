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
        const int syncFrequencyMinutes = 1;

        [STAThread]
        static void Main(string[] args)
        {
            FileStream ostrm;
            StreamWriter writer;
            TextWriter oldOut = Console.Out;
            try
            {
                var downloadPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)
            , "ExactSync", "Log.txt");
                (new FileInfo(downloadPath)).Directory.Create();
                ostrm = new FileStream(downloadPath, FileMode.OpenOrCreate, FileAccess.Write);
                writer = new StreamWriter(ostrm);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot open Log.txt for writing");
                Console.WriteLine(e.Message);
                return;
            }
            Console.SetOut(writer);

            try
            {

                ExactSync sync = new ExactSync();
                do
                {
                    while (!Console.KeyAvailable)
                    {
                        sync.DownloaderDoWork();

                        sync.UploaderDoWork();

                        Thread.Sleep(syncFrequencyMinutes * 60 * 1000);
                    }
                } while (Console.ReadKey(true).Key != ConsoleKey.Escape );

            }

            catch (Exception ex)
            {
                Console.WriteLine("Failed to Sync with exception :" + ex.GetBaseException().ToString());

                Console.ReadKey();
            }
            finally
            {
                Console.SetOut(oldOut);
                writer.Close();
                ostrm.Close();
                Console.WriteLine("Done");
            }
        }

     

    }
}
