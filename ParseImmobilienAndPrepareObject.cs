using HtmlAgilityPack;
using System.Collections.Generic;

namespace TheWebScraper
{
    class ParseImmobilienAndPrepareObject
    {
        public List<Dictionary<string, string>> ListOfImmobilienProperties;
        Dictionary<string, string> ImmobilienProperties;

        public void ParseAndReturnObject(string immobilienscoutUrl, string homePage)
        {
            ListOfImmobilienProperties = new List<Dictionary<string, string>>();

            HtmlWeb immobilienscoutWeb = new HtmlWeb();
            HtmlDocument htmlDoc = immobilienscoutWeb.Load(immobilienscoutUrl);
            HtmlNodeCollection nodeCollectionLinks = htmlDoc.DocumentNode.SelectNodes("//article/div/div/div/div/a");

            foreach (HtmlNode link in nodeCollectionLinks)
            {
                ImmobilienProperties = new Dictionary<string, string>();

                string propertiesLink = homePage + link.Attributes["href"].Value;

                HtmlWeb propertyWeb = new HtmlWeb();
                HtmlDocument htmlPropertyDoc = propertyWeb.Load(propertiesLink);

                //title
                HtmlNodeCollection titles = htmlPropertyDoc.DocumentNode.SelectNodes("//div/div/div/div/div/h1");
                ImmobilienProperties[Constants.title] = titles[0].InnerText;

                //etage
                string etageVon = "0";
                string etageBis = "0";
                string dbType = "";
                HtmlNodeCollection types = htmlPropertyDoc.DocumentNode.SelectNodes("//div/div/div/div/div/div/div/div/dl");
                ImmobilienProperties[Constants.etageNummer] = etageVon;
                ImmobilienProperties[Constants.etageVon] = etageBis;

                foreach (HtmlNode type in types)
                {
                    if (type.InnerText.Contains(Constants.etage) && !type.InnerText.Contains(Constants.etagenheizung))
                    {
                        string[] etagen = type.InnerText.Split(' ');
                        etageVon = string.IsNullOrWhiteSpace(etagen[3]) ? "0" : etagen[3];
                        etageBis = string.IsNullOrWhiteSpace(etagen[5]) ? "0" : etagen[5];
                        ImmobilienProperties[Constants.etageNummer] = etageVon;
                        ImmobilienProperties[Constants.etageVon] = etageBis;
                    }
                    dbType = type.InnerText;
                }
                ListOfImmobilienProperties.Add(ImmobilienProperties);
            }
        }
    }
}