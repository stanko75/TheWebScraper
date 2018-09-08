using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWebScraper
{
    public class Bezugsfrei: BaseParsingClass
    {
        public virtual void GetParsedHtml(HtmlNode type, string htmlName, string dbName, int[] indexes, Dictionary<string, string> immobilienProperties)
        {
            foreach (int index in indexes)
            {
                if (type.InnerText.Trim().ToLower().Contains(htmlName.ToLower()))
                {
                    string[] typ = type.InnerText.Trim().Split(' ');

                    if (typ.Length > index)
                    {
                        immobilienProperties[dbName] = typ[index];
                    }
                    else
                    {
                        immobilienProperties[dbName] = type.InnerText.Trim();
                    }
                }
            }
        }
    }
}
