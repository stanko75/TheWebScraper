using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Collections.Generic;

namespace TheWebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            ParseImmobilienAndPrepareObject properties = new ParseImmobilienAndPrepareObject();

            NameValueCollection links = ConfigurationManager.GetSection(@"links") as NameValueCollection;
            string homePage = ConfigurationManager.AppSettings.Get("homePage");

            foreach (string link in links)
            {
                properties.ParseAndReturnObject(links.Get(link), homePage);
            }

            DbManagement dbManagement = new DbManagement();

            foreach (Dictionary<string, string> immobilienProperties in properties.ListOfImmobilienProperties)
            {
                if (dbManagement.PropertyExists(immobilienProperties["link"]))
                {
                    dbManagement.UpdateDb(immobilienProperties["link"]);
                }
                else
                {
                    dbManagement.InsertIntoDb(immobilienProperties);
                }
            }

            Console.WriteLine("Press any key");
            Console.ReadKey();
        }
    }
}
