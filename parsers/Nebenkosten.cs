using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWebScraper
{
    public class Nebenkosten : BaseParsingClass
    {
        public virtual void GetParsedHtml(HtmlNode type, string htmlName, string dbName, int[] indexes, Dictionary<string, string> immobilienProperties)
        {
            Debug.WriteLine("test");
            foreach (int index in indexes)
            {
                if (type.InnerText.Trim().ToLower().Contains(htmlName.ToLower()) && !type.InnerText.Trim().ToLower().Contains("heizkosten"))
                {
                    string[] typ = type.InnerText.Trim().Split(' ');
                    immobilienProperties[dbName] = typ[index];
                }
            }
        }
    }
}