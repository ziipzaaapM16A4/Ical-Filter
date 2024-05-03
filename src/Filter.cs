using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IcalFilter
{
    internal static class Filter
    {
        internal static bool FilterIcal()
        {
            try
            {
                Console.WriteLine("\nFiltering Events");

                StreamReader sr = new StreamReader("../processingfolder/events.ical");
                StreamWriter sw = new StreamWriter("../processingfolder/modifiedevents.ical");
                bool stopping = false;
                bool filterTriggert = false;
                List<string> lines = new List<string>();
                string line;

                while (!stopping)
                {
                    line = sr.ReadLine();
                    if (line == "BEGIN:VCALENDAR"
                        || line.Contains("X - WR - CALNAME:")
                        || line.Contains("VERSION")
                        || line.Contains("PRODID:")
                        || line.Contains("METHOD:")
                        || line.Contains("X-WR-RELCALID")
                        )
                    {
                        sw.WriteLine(line);
                    }
                    else if (line.Contains("BEGIN:VEVENT")
                        || line.Contains("LOCATION:")
                        || line.Contains("SUMMARY:")
                        || line.Contains("DESCRIPTION:")
                        || line.Contains("DTSTART")
                        || line.Contains("DTEND:")
                        || line.Contains("DTSTAMP:")
                        || line.Contains("UID:")
                        || line.Contains("END:VEVENT")
                        )
                    {
                        lines.Add(line);

                        if (line.Contains("SUMMARY:"))
                        {
                            if (line.Contains("Urlaub") 
                                || line.Contains("Dienstfrei")
                                || line.Contains("Verfügbar Tag")
                                || line.Contains("Verfügbar Nacht")
                                || line.Contains("Verfügbar Früh")
                                || line.Contains("Verfügbar Spät")
                                )
                            {
                                filterTriggert = true;
                            }
                        }
                        else if (line.Contains("END:VEVENT"))
                        {
                            if (!filterTriggert)
                            {
                                while (lines.Count != 0)
                                {
                                    sw.WriteLine(lines.First());
                                    lines.RemoveAt(0);
                                }
                            }
                            else
                            {
                                lines.Clear();
                            }
                            filterTriggert = false;
                        }

                    }
                    else if (line == "END:VCALENDAR")
                    {
                        sw.WriteLine(line);
                        stopping = true;
                    }

                }

                sr.Close();
                sw.Close();
                Console.WriteLine("Successfully filtered events");
                return true;
            } catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: Failed filtering events: " + ex.Message);
                Console.ResetColor();
                return false;
            }
        }
    }
}
