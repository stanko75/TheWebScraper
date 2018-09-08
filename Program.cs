using HtmlAgilityPack;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Configuration;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Diagnostics;

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

                /*
                Console.WriteLine("Title: " + tt[Constants.Db.title]);
                Console.WriteLine("Etage: " + tt[Constants.Db.etageNummer] + " von " + tt[Constants.Db.etageVon]);
                */
            }

            Console.WriteLine("Press any key");
            Console.ReadKey();

            ////////////////////////////////////////

            //string immobilienscoutUrl = links.Get("link1");

            //HtmlWeb immobilienscoutWeb = new HtmlWeb();

            //HtmlDocument htmlDoc = immobilienscoutWeb.Load(immobilienscoutUrl);

            //HtmlNodeCollection nodeCollectionLinks = htmlDoc.DocumentNode.SelectNodes("//article/div/div/div/div/a");
            //string path = @"c:\tmp";
            //string fileName = path + @"\tmp.txt";
            //StreamWriter sw = File.CreateText(fileName);

            //string connectionString = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
            //SqlConnection cn = new SqlConnection(connectionString);
            //SqlCommand insertCommand;
            //cn.Open();
            ////ServerConnection svrConnection = new ServerConnection(cn);
            ////Server server = new Server(svrConnection);

            ////File.WriteAllLines(fileNmae, nodes.ToString());

            //WebClient client = new WebClient();

            //int i = 0;
            //foreach (HtmlNode link in nodeCollectionLinks)
            //{
            //    i++;

            //    string propertiesLink = homePage + link.Attributes["href"].Value;
            //    string dbTitle = "";
            //    string dbType = "";
            //    string etageVon = "0";
            //    string etageBis = "0";

            //    Console.WriteLine("OuterHtml: " + propertiesLink);

            //    HtmlWeb propertyWeb = new HtmlWeb();
            //    HtmlDocument htmlPropertyDoc = propertyWeb.Load(propertiesLink);
            //    HtmlNodeCollection titles = htmlPropertyDoc.DocumentNode.SelectNodes("//div/div/div/div/div/h1");

            //    foreach (var title in titles)
            //    {
            //        Console.WriteLine("Title: " + title.InnerText);
            //        dbTitle = title.InnerText;
            //    }

            //    HtmlNodeCollection types = htmlPropertyDoc.DocumentNode.SelectNodes("//div/div/div/div/div/div/div/div/dl");
            //    foreach (var type in types)
            //    {
            //        Console.WriteLine("Type: " + type.InnerText);
            //        if (type.InnerText.Contains("Etage") && !(type.InnerText.Contains("Etagenheizung")))
            //        {
            //            string[] etagen = type.InnerText.Split(' ');
            //            etageVon = string.IsNullOrWhiteSpace(etagen[3]) ? "0" : etagen[3];
            //            etageBis = string.IsNullOrWhiteSpace(etagen[5]) ? "0" : etagen[5];
            //        }
            //        dbType = type.InnerText;
            //    }

            //    int intEtageVon = 0;
            //    int intEtageBis = 0;

            //    int.TryParse(etageVon, out intEtageVon);
            //    int.TryParse(etageBis, out intEtageBis);

            //    string checkIfLinkExists = $"select link from immobilien where link = '{propertiesLink}'";
            //    SqlCommand selectCommand = new SqlCommand(checkIfLinkExists, cn);
            //    SqlDataReader selectReader = selectCommand.ExecuteReader();

            //    if (!selectReader.Read())
            //    {
            //        selectReader.Close();
            //        string insertCommandString = $"insert into immobilien (link, title, etageVon, etageBis, inserted, updated) values (" +
            //            "@propertiesLink, @dbTitle, @etageVon, @etageBis, @inserted, @updated)";
            //        insertCommand = new SqlCommand(insertCommandString, cn);

            //        insertCommand.Parameters.AddWithValue("@propertiesLink", propertiesLink);
            //        insertCommand.Parameters.AddWithValue("@dbTitle", dbTitle);
            //        insertCommand.Parameters.AddWithValue("@etageVon", intEtageVon.ToString());
            //        insertCommand.Parameters.AddWithValue("@etageBis", intEtageBis.ToString());
            //        insertCommand.Parameters.AddWithValue("@inserted", DateTime.Now);
            //        insertCommand.Parameters.AddWithValue("@updated", DateTime.Now);

            //        insertCommand.ExecuteNonQuery();
            //    }
            //    else
            //    {
            //        selectReader.Close();

            //        string updateCommandString = $"update immobilien set updated = @updated where link = @propertiesLink";

            //        SqlCommand updateCommand;

            //        updateCommand = new SqlCommand(updateCommandString, cn);

            //        updateCommand.Parameters.AddWithValue("@propertiesLink", propertiesLink);
            //        updateCommand.Parameters.AddWithValue("@updated", DateTime.Now);

            //        updateCommand.ExecuteNonQuery();
            //    }

            //}

            //cn.Close();

            //Console.WriteLine("Press any key");
            //Console.ReadKey();
        }
    }
}
