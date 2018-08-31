using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Reflection;

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
                ImmobilienProperties["link"] = propertiesLink;

                HtmlWeb propertyWeb = new HtmlWeb();
                HtmlDocument htmlPropertyDoc = propertyWeb.Load(propertiesLink);

                //title
                HtmlNodeCollection titles = htmlPropertyDoc.DocumentNode.SelectNodes("//div/div/div/div/div/h1");
                ImmobilienProperties[Constants.Db.title] = titles[0].InnerText;

                //etage
                string dbType = "";
                HtmlNodeCollection types = htmlPropertyDoc.DocumentNode.SelectNodes("//div/div/div/div/div/div/div/div/dl");

                foreach (HtmlNode type in types)
                {
                    //Etage
                    if (type.InnerText.Contains(Constants.Html.etage)
                        && !type.InnerText.Contains(Constants.Html.etagenheizung)
                        && !type.InnerText.Contains(Constants.Html.typ)
                    )
                    {
                        GetParsedHtml(type, Constants.Html.etage, Constants.Db.typ, new int[] { 2 }, ImmobilienProperties);
                    }

                    //Typ
                    GetParsedHtml(type, Constants.Html.typ, Constants.Db.typ, new int[] { 2 }, ImmobilienProperties);

                    //Wohnflaeche
                    GetParsedHtml(type, Constants.Html.wohnflaeche, Constants.Db.wohnflaeche, new int[] { 3 }, ImmobilienProperties);

                    //Bezugsfrei 
                    GetParsedHtml(type, Constants.Html.bezugsfrei, Constants.Db.bezugsfrei, new int[] {3}, ImmobilienProperties);

                    //Bonitätsauskunft
                    GetParsedHtml(type, Constants.Html.bonitaetsauskunft, Constants.Db.bonitaetsauskunft, new int[] { 2 }, ImmobilienProperties);

                    //Zimmer
                    GetParsedHtml(type, Constants.Html.zimmer, Constants.Db.zimmer, new int[] { 2 }, ImmobilienProperties);

                    //Kaltmiete
                    GetParsedHtml(type, Constants.Html.kaltmiete, Constants.Db.kaltmiete, new int[] { 2 }, ImmobilienProperties);

                    //Nebenkosten
                    GetParsedHtml(type, Constants.Html.nebenkosten, Constants.Db.nebenkosten, new int[] { 3 }, ImmobilienProperties);

                    dbType = type.InnerText;
                }
                ListOfImmobilienProperties.Add(ImmobilienProperties);
            }
        }

        private void GetParsedHtml(HtmlNode type, string htmlName, string dbName, int[] indexes, Dictionary<string, string> immobilienProperties)
        {
            Type htmlType = Type.GetType("TheWebScraper." + htmlName);
            if (htmlType is null)
            {
                htmlType = typeof(BaseParsingClass);
            }
            
            MethodInfo methodInfo = htmlType.GetMethod("GetParsedHtml");
            object classInstance = Activator.CreateInstance(htmlType, null);

            object[] parametersArray = new object[] { type, htmlName, dbName, indexes, immobilienProperties };
            methodInfo.Invoke(classInstance, parametersArray);
        }
    }
}