using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

namespace TheWebScraper
{
    public class DbManagement
    {
        private SqlConnection DbConnection
        {
            get
            {
                string connectionString = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
                return new SqlConnection(connectionString); 
            }
        }


        public bool PropertyExists(string propertiesLink)
        {
            string checkIfLinkExists = $"select link from immobilien where link = '{propertiesLink}'";
            SqlConnection cn = DbConnection;

            cn.Open();

            SqlCommand selectCommand = new SqlCommand(checkIfLinkExists, cn);
            SqlDataReader selectReader = selectCommand.ExecuteReader();

            bool exists = selectReader.Read();

            cn.Close();

            return exists;
        }

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

            SqlConnection conn = DbConnection;

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

        internal void UpdateDb(string propertiesLink)
        {
            string updateCommandString = $"update immobilien set updated = @updated where link = @propertiesLink";

            SqlCommand updateCommand;

            SqlConnection cn = DbConnection;

            cn.Open();

            updateCommand = new SqlCommand(updateCommandString, cn);
            updateCommand.Parameters.AddWithValue("@propertiesLink", propertiesLink);
            updateCommand.Parameters.AddWithValue("@updated", DateTime.Now);


            updateCommand.ExecuteNonQuery();

            cn.Close();
        }
    }
}