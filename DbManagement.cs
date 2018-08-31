using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWebScraper
{
    public class DbManagement
    {
        public void CreateSqlQuery(Dictionary<string, string> immobilienProperties)
        {
            string fieldNames = "";
            string values = "";
            foreach (KeyValuePair<string, string> property in immobilienProperties)
            {
                fieldNames = string.IsNullOrWhiteSpace(fieldNames) ? "[" + property.Key + "]" : fieldNames + ", " + "[" + property.Key + "]";
                values = string.IsNullOrWhiteSpace(fieldNames) ? "'" + property.Value + "'" : values + ", " + "'" + property.Value + "'";
            }

            string sql = "INSERT INTO[dbo].[immobilien] (" + fieldNames + ") VALUES (" + values + ")";
        }
    }
}