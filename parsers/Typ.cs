using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWebScraper
{
    public class Typ : BaseParsingClass
    {
        public virtual void GetParsedHtml(HtmlNode type, string htmlName, string dbName, int[] indexes, Dictionary<string, string> immobilienProperties)
        {
            foreach (int index in indexes)
            {
                if (type.InnerText.Trim().ToLower().Contains(htmlName.ToLower()) && !type.InnerText.Trim().ToLower().Contains("energie"))
                {
                    string[] typ = type.InnerText.Trim().Split(' ');
                    immobilienProperties[dbName] = typ[index];
                }
            }
        }
    }
}
