using IcalFilter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IcalFilter
{
    internal class Uploader
    {
        internal static bool UploadIcal()
        {
            try
            {
                Console.WriteLine($"\nUploading modified Ical");
                using (var client = new WebClient())
                {
                    string link = "ftp://" + Program.uploadURL + (Program.uploadURL.Last() != char.Parse("/") ? "/" : "") + "events.ical";
                    client.Credentials = new NetworkCredential(Program.username, Program.password);
                    byte[] result = client.UploadFile(link, WebRequestMethods.Ftp.UploadFile, "../processingfolder/modifiedevents.ical");
                    if (result.Length > 0) Console.WriteLine($"INFO: WebClient: {System.Text.Encoding.ASCII.GetString(result)}");
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Successfully uploaded modified Ical!");
                Console.ResetColor();
                return true;
            } catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ERROR: Failed uploading Ical: {e.Message}");
                Console.ResetColor();
                return false;
            }
        }
    }
}

