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
            connectionString = $"Host={Env.db_host}; " +
                $"Username={Env.db_username}; " +
                $"Password={Env.db_pass}; " +
                $"Database={Env.database_name}; " +
                $"Port={Env.database_port};";
        }

        /// <summary>
        /// Execute function in db to insert xml into tabels
        /// </summary>
        /// <param name="xml">string with xml data</param>
        /// <returns> </returns>
        public async Task InsertXmlToDb(string xml)
        {
            if (IsLastXMLEqualToCurrent(xml).Result)
            {
                return;
            }
            try
            {
                using (var con = new NpgsqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = con;

                        cmd.CommandText = "CALL public.proc_insert_xml_data(@xml::xml);";
                        cmd.Parameters.AddWithValue("xml", xml);

                        cmd.Prepare();

                        cmd.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception e)
            {
                Scheduler.WriteToErrorLog(e.ToString(), System.Diagnostics.EventLogEntryType.Error);
            }

        }

        private async Task<bool> IsLastXMLEqualToCurrent(string currentXml)
        {
            try
            {
                using (var con = new NpgsqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = con;

                        cmd.CommandText = "SELECT public.func_check_if_last_xml_equal_to_current(@xml::xml) AS result;";
                        cmd.Parameters.AddWithValue("xml", currentXml);

                        cmd.Prepare();

                        return (bool)cmd.ExecuteScalar();


                    }
                }
            }
            catch (Exception e)
            {
                
                Scheduler.WriteToErrorLog(e.ToString(), System.Diagnostics.EventLogEntryType.Error);
                return true; //return true to just return null in next method
            }
        }
    }
}
