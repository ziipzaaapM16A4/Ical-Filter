using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IcalFilter
{
    internal class ConfigReader
    {
        public static bool ConfigReading()
        {
            Console.WriteLine("\nInitialize configuration");
            try
            {
                if (!System.IO.Directory.Exists("../config/"))           System.IO.Directory.CreateDirectory("../config/");
                if (!System.IO.Directory.Exists("../processingfolder/")) System.IO.Directory.CreateDirectory("../processingfolder/");

                XmlReader xmlReader = XmlReader.Create("../config/config.xml");
                if (xmlReader.ReadToDescendant("Config"))
                    while (xmlReader.Read())
                    {
                        if (xmlReader.IsStartElement())
                        {

                            switch (xmlReader.Name.ToString())
                            {
                                case "DownloadURL":
                                    Program.downloadURL = xmlReader.ReadString();
                                    Console.WriteLine("Download URL is: " + Program.downloadURL);
                                    break;
                                case "UploadURL":
                                    Program.uploadURL = xmlReader.ReadString();
                                    Console.WriteLine("Upload URL is: " + Program.uploadURL);
                                    break;
                                case "Username":
                                    Program.username = xmlReader.ReadString();
                                    Console.WriteLine("Username is: " + Program.username);
                                    break;
                                case "Password":
                                    Program.password = xmlReader.ReadString();
                                    Console.WriteLine("Password is set. Not Showing here due to Security Reasons");
                                    break;
                                case "IdleTime":
                                    Program.idleTime = int.Parse(xmlReader.ReadString());
                                    Console.WriteLine("Idle Time is: " + Program.idleTime);
                                    break;
                                case "FilterEvents":
                                    string eventsXml = xmlReader.ReadInnerXml();
                                    Program.filterEvents = GetEventsFromXml(eventsXml);
                                    Console.WriteLine("Filter Events:");
                                    foreach (string e in Program.filterEvents)
                                    {
                                        Console.WriteLine(e);
                                    }
                                    break;
                            }
                        }
                    }


                Console.WriteLine("Successfully initialized configuration!");
                return true;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: Failed reading configuration: " + e.Message);
                Console.ResetColor();
                return false;
            }
        }
        static string[] GetEventsFromXml(string xml)
        {
            string[] events = xml.Split(',');
            for (int i = 0; i < events.Length; i++)
            {
                events[i] = events[i].Trim();
            }
            return events;
        }

    }
}
