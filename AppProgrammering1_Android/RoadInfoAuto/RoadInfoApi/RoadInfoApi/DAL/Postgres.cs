using Npgsql;
using RoadInfoApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadInfoApi.DAL
{
    public class Postgres
    {
        private string connectionString;

        public Postgres()
        {
            DotNetEnv.Env.Load();


            connectionString = $"Host={Environment.GetEnvironmentVariable("DB_HOST")}; " +
                $"Username={Environment.GetEnvironmentVariable("DB_USERNAME")}; " +
                $"Password={Environment.GetEnvironmentVariable("DB_PASS")}; " +
                $"Database={Environment.GetEnvironmentVariable("DATABASE_NAME")}; " +
                $"Port={Environment.GetEnvironmentVariable("DATABASE_PORT")};";
        }



        #region Queries 



        public async Task<List<Situation>> GetSituations(double currentLongi, double currentLati, string username, bool forceGet = false)
        {
            //TODO: Add limit (5)
            //TODO: long and lati inputs to calculate distance
            List<Situation> situations = new List<Situation>();
            try
            {
                await using var con = new NpgsqlConnection(connectionString);
                await con.OpenAsync();


                if (!await CheckForNewData(con, username) && !forceGet)
                {
                    return situations;
                }


                await using var cmd = new NpgsqlCommand("SELECT * FROM func_get_closest_situations($1, $2);", con)
                {
                    Parameters =
                        {
                        new() { Value = currentLongi },
                        new() { Value = currentLati }
                        }
                };
                await using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    situations.Add(new Situation(
                        reader["s_id"].ToString(),
                        (Int64)reader["s_version"],
                        (DateTime)reader["s_situationversiontime"],
                        reader["s_locationtext"].ToString(),
                        reader["s_eventtext"].ToString(),
                        reader["s_regiontext"].ToString(),
                        reader["s_noncodeableinformationtext"].ToString(),
                        reader["s_durationtext"].ToString(),
                        reader["s_icon"].ToString(),
                        reader["s_status"].ToString())
                    );
                }
                reader.Close();
                await CreateOrUpdateUserActivity(con, username);
            }
            catch (Exception e)
            {
                //Scheduler.WriteToErrorLog(e.ToString(), System.Diagnostics.EventLogEntryType.Error);
                Console.WriteLine(e.ToString());
                return null;

            }


            return situations;
        }

        public async Task<bool> CheckForNewData(NpgsqlConnection con, string username)
        {
            try
            {
                await using var cmd = new NpgsqlCommand("SELECT func_any_new_data_since_lastactivity($1);", con)
                {
                    Parameters =
                        {
                        new() { Value = username }
                        }
                };
                return Convert.ToBoolean(await cmd.ExecuteScalarAsync());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }

        }

        public async Task CreateOrUpdateUserActivity(NpgsqlConnection con, string username)
        {
            try
            {
                await using var cmd = new NpgsqlCommand("CALL proc_update_user_activity($1);", con)
                {
                    Parameters =
                        {
                        new() { Value = username }
                        }
                };
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        public async Task<bool> CheckIfUsernameIsValid(NpgsqlConnection con, string username)
        {
            try
            {
                await using var cmd = new NpgsqlCommand("SELECT func_is_username_valid($1);", con)
                {
                    Parameters =
                        {
                        new() { Value = username }
                        }
                };

                var test = await cmd.ExecuteScalarAsync();
                return Convert.ToBoolean( test);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }

        }

        public NpgsqlConnection GetConnection()
        {
            try
            {
                var con = new NpgsqlConnection(connectionString);
                con.Open();

                return con;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public void CloseConnection(NpgsqlConnection con)
        {
            try
            {
                con.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public async Task<List<Situation>> GetSituationRecords(List<Situation> situations)
        {
            try
            {
                await using var con = new NpgsqlConnection(connectionString);
                await con.OpenAsync();


                foreach (Situation s in situations)
                {
                    List<SituationRecord> situationrecords = new List<SituationRecord>();

                    await using var cmd = new NpgsqlCommand("SELECT * FROM func_get_situationrecords($1);", con)
                    {
                        Parameters =
                        {
                            new() {Value = s.Id}
                        }
                    };

                    await using var reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        situationrecords.Add(new SituationRecord(
                            reader["sr_id"].ToString(),
                            (Int64)reader["sr_version"],
                            reader["sr_situationId"].ToString(),
                            Convert.IsDBNull(reader["sr_situationrecordcreationtime"]) ? null : (DateTime?)reader["sr_situationrecordcreationtime"],
                            Convert.IsDBNull(reader["sr_situationrecordversiontime"]) ? null : (DateTime?)reader["sr_situationrecordversiontime"],
                            reader["sr_validitystatus"].ToString(),
                            Convert.IsDBNull(reader["sr_startofperiod"]) ? null : (DateTime?)reader["sr_startofperiod"],
                            Convert.IsDBNull(reader["sr_endofperiod"]) ? null : (DateTime?)reader["sr_endofperiod"],
                            reader["sr_generalcomment_description"].ToString(),
                            reader["sr_generalcomment_locationdescriptor"].ToString(),
                            reader["sr_generalcomment_other"].ToString(),
                            Convert.IsDBNull(reader["sr_latitude"]) ? null : (double?)reader["sr_latitude"],
                            Convert.IsDBNull(reader["sr_longitude"]) ? null : (double?)reader["sr_longitude"],
                            Convert.IsDBNull(reader["sr_temporaryspeedlimit"]) ? null : (int?)reader["sr_temporaryspeedlimit"],
                            reader["sr_carparkidentity"].ToString(),
                            reader["sr_carparkstatus"].ToString(),
                            reader["sr_publiceventtype"].ToString(),
                            reader["sr_type"].ToString())
                        );
                    }


                    s.SituationRecords = situationrecords;
                }
            }
            catch (Exception e)
            {
                //Scheduler.WriteToErrorLog(e.ToString(), System.Diagnostics.EventLogEntryType.Error);
                Console.WriteLine(e.ToString());
                return null;

            }


            return situations;
        }


        #endregion

    }
}
