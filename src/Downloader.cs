using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IcalFilter
{
    internal static class Downloader
    {
        internal static bool DownloadIcal()
        {
            Console.WriteLine($"\nDownloading Ical");

            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile(Program.downloadURL, "../processingfolder/events.ical");
                }
                Console.WriteLine($"Successfully downloaded Ical.");
                return true;

            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ERROR: Failed downloading Ical: {e.Message}");
                Console.ResetColor();
                return false;
            }
        }
    }
}
