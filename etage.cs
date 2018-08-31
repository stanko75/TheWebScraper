using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWebScraper
{
    public class Etage : BaseParsingClass
    {
        public override void GetParsedHtml(HtmlNode type, string htmlName, string dbName, int[] indexes, Dictionary<string, string> immobilienProperties)
        {
            string etageVon = "0";
            string etageBis = "0";

            string[] etagen = type.InnerText.Trim().Split(' ');
            if (etagen.Length > 3)
            {
                etageVon = string.IsNullOrWhiteSpace(etagen[2]) ? "0" : etagen[2];
                etageBis = string.IsNullOrWhiteSpace(etagen[4]) ? "0" : etagen[4];
            }
            else
            {
                etageVon = string.IsNullOrWhiteSpace(etagen[2]) ? "0" : etagen[2];
            }
            immobilienProperties[Constants.Db.etageNummer] = etageVon;
            immobilienProperties[Constants.Db.etageVon] = etageBis;
        }
    }
}
