using HtmlAgilityPack;
using System.Collections.Generic;

namespace TheWebScraper
{
    public class BaseParsingClass
    {
        public virtual void GetParsedHtml(HtmlNode type, string htmlName, string dbName, int[] indexes, Dictionary<string, string> immobilienProperties)
        {
            foreach (int index in indexes)
            {
                if (type.InnerText.Trim().ToLower().Contains(htmlName.ToLower()))
                {
                    string[] typ = type.InnerText.Trim().Split(' ');
                    immobilienProperties[dbName] = typ[index];
                }
            }
        }
    }
}
