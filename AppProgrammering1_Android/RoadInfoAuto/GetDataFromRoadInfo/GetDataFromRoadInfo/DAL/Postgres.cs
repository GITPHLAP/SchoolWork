using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDataFromRoadInfo.DAL
{
    class Postgres
    {
        string connectionString;


        public Postgres()
        {
            connectionString = $"Host={ConfigurationManager.AppSettings["dbHost"]}; " +
                $"Username={ConfigurationManager.AppSettings["dbUsername"]}; " +
                $"Password={ConfigurationManager.AppSettings["dbPass"]}; " +
                $"Database={ConfigurationManager.AppSettings["databaseName"]}; " +
                $"Port=5433";
        }
        /// <summary>
        /// Execute function in db to insert xml into tabels
        /// </summary>
        /// <param name="xml">string with xml data</param>
        /// <returns> </returns>
        public async Task InsertXmlToDb(string xml)
        {
            try
            {
                using (var con = new NpgsqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = con;

                        //TODO: call the right procedure 
                        //cmd.CommandText = "SELECT @par AS testPar";
                        cmd.CommandText = "CALL public.proc_insert_xml_data(@xml::xml);";
                        cmd.Parameters.AddWithValue("xml", xml);
                        //cmd.Parameters.AddWithValue("par", "Hej");

                        cmd.Prepare();

                        cmd.ExecuteNonQuery();
                        //int test = cmd.ExecuteNonQuery();

                        //Console.WriteLine(test);
                    }
                }
            }
            catch (Exception e)
            {
                Scheduler.WriteToErrorLog(e.ToString(), System.Diagnostics.EventLogEntryType.Error);
            }
            
        }
    }
}
