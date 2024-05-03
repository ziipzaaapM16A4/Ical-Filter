using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IcalFilter
{
    internal class Program
    {
        internal static string downloadURL;

        internal static string uploadURL;
        internal static string username;
        internal static string password;
        internal static int idleTime = 15;
        private static bool once = false;

        internal static string[] filterEvents = new string[] {};

        static int Main(string[] args)
        {
            Console.WriteLine($"Starting ical filter");
            if (args.Length > 0) if (args[0] == "test") once = true;
            Thread.Sleep(2500);

            while (true) {
                Console.WriteLine($"Starting new Iteration");
                if (ConfigReader.ConfigReading()) {
                    if (Downloader.DownloadIcal()) {
                        if (Filter.FilterIcal()) {
                            if (Uploader.UploadIcal()) {
                                if (once) return 0;
                            } else { if (once) return 4; }
                        } else { if (once) return 3; }
                    } else { if (once) return 2; }
                } else { if (once) return 1; }
                
                System.Threading.Thread.Sleep(idleTime * 60 * 1000);
                //IdleAnimation();
            }
        }

        private static void IdleAnimation()
        {

            Console.WriteLine("");
            DateTime startTime = DateTime.Now;
            while ((DateTime.Now - startTime).TotalMilliseconds < idleTime * 60 * 1000)
            {
                Console.Write("\rIcal filter: Idling   ");
                System.Threading.Thread.Sleep(250);
                Console.Write("\rIcal filter: Idling.");
                System.Threading.Thread.Sleep(250);
                Console.Write("\rIcal filter: Idling..");
                System.Threading.Thread.Sleep(250);
                Console.Write("\rIcal filter: Idling...");
                System.Threading.Thread.Sleep(250);
            }
            Console.Write("\r                        ");
            Console.WriteLine("");
        }
    }
}
