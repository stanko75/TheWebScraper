using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWebScraper
{
    public class DbManagement
    {
        public void InsertIntoDb(Dictionary<string, string> immobilienProperties)
        {
            IOrderedEnumerable<KeyValuePair<string, string>> immobilienPropertiesOrdered = immobilienProperties.OrderBy(order => order.Key);

            string fieldNames = "";
            string values = "";
            foreach (KeyValuePair<string, string> property in immobilienPropertiesOrdered)
            {
                fieldNames = string.IsNullOrWhiteSpace(fieldNames) ? "[" + property.Key + "]" : fieldNames + ", " + "[" + property.Key + "]";
                values = string.IsNullOrWhiteSpace(values) ? "@" + property.Key : values + ", " + "@" + property.Key;
            }

            fieldNames = fieldNames + ", [inserted], [updated]";
            values = values + ", @inserted, @updated";

            string sql = "INSERT INTO[dbo].[immobilien] (" + fieldNames + ") VALUES (" + values + ")";

            string connectionString = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            SqlCommand insertCommand = new SqlCommand(sql, conn);

            foreach (KeyValuePair<string, string> property in immobilienPropertiesOrdered)
            {
                if (property.Key.ToLower() != "html")
                {
                    Debug.WriteLine("property.Key: " + property.Key + ", property.Value: " + property.Value);
                }

                if ((property.Key == Constants.Db.wohnflaeche.ToLower()) 
                    || (property.Key == Constants.Db.gesamtmiete.ToLower()) 
                    || (property.Key == Constants.Db.kaltmiete.ToLower())
                    || (property.Key == Constants.Db.zimmer.ToLower())
                    || (property.Key == Constants.Db.nebenkosten.ToLower())
                )
                {
                    decimal value = 0;
                    decimal.TryParse(property.Value, out value);
                    insertCommand.Parameters.AddWithValue("@" + property.Key, value);
                }
                else
                {
                    insertCommand.Parameters.AddWithValue("@" + property.Key, property.Value);
                }
            }

            insertCommand.Parameters.AddWithValue("@inserted", DateTime.Now);
            insertCommand.Parameters.AddWithValue("@updated", DateTime.Now);

            insertCommand.ExecuteNonQuery();
        }

    }
}