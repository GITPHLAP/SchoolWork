using Npgsql;
using SecureLoginWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecureLoginWebAPI
{
    public class Postgres
    {
        static string connectionString = "";

        public static async void CreateUser(User user)
        {
            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand("INSERT INTO secure_login_users VALUES(@p1, @p2, @p3)", conn)
            {
                Parameters =
                {
                    new("p1", user.UserName),
                    new("p2", user.Hashed_Password),
                    new("p3", user.Salt)

                }
            };

            await cmd.ExecuteNonQueryAsync();
        }

        public static async Task<bool> IsUsernameUnique(string username)
        {
            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand("SELECT COUNT(1) FROM secure_login_users WHERE username = @p1;", conn)
            {
                Parameters =
                {
                    new("p1", username),

                }
            };

            Int64 count = (Int64)await cmd.ExecuteScalarAsync();

            return count == 0;
        }

        public static async Task<bool> ValidateUserCrendentials(User user)
        {
            //First get salt from user
            byte[] salt = Convert.FromBase64String(await GetUsersSalt(user.UserName));

            //Then we hash the password and salt from user
            string hashed_password = Convert.ToBase64String(Hash.HashPasswordWithSalt(user.Plain_password, salt));

            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand("SELECT COUNT(1) FROM secure_login_users WHERE hashed_pw = @p1;", conn)
            {
                Parameters =
                {
                    new("p1", hashed_password),

                }
            };

            Int64 count = (Int64)await cmd.ExecuteScalarAsync();

            return count == 1; //if count == 1 then its valid
        }


        private static async Task<string> GetUsersSalt(string username)
        {
            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand("SELECT salt FROM secure_login_users WHERE username = @p1;", conn)
            {
                Parameters =
                {
                    new("p1", username),

                }
            };

            string salt = (string)await cmd.ExecuteScalarAsync();

            return salt;
        }





        #region User queries

        public static async void CreateUser2(ApplicationUser user)
        {
            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();
            
            await using var cmd = new NpgsqlCommand("INSERT INTO applicationuser(username, normalizedusername, email, " +
                "normalizedemail, emailconfirmed, passwordhash, securitystamp, concurrencystamp, phonenumber, " +
                "phonenumberconfirmed, twofactorenabled, lockoutend, lockoutenable, accessfailedcount)" +
                " VALUES(@p2, @p3, @p4, @p5, CAST(@p6 AS BIT), @p7," +
                " @p8, @p9, @p10, CAST(@p11 AS BIT), CAST(@p12 AS BIT), @p13, CAST(@p14 AS BIT), @p15)", conn)
            {
                Parameters =
                {
                    new("p2", user.Username),
                    new("p3", user.Normalizedusername),
                    new("p4", user.Email),
                    new("p5", user.Normalizedemail),
                    new("p6", Convert.ToString(user.Emailconfirmed ? 1 : 0)),
                    new("p7", user.Passwordhash),
                    new("p8", user.SecurityStamp == null ? DBNull.Value : user.SecurityStamp),
                    new("p9", user.ConcurrencyStamp == null ? DBNull.Value : user.ConcurrencyStamp),
                    new("p10", user.Phonenumber == null ? DBNull.Value : user.Phonenumber),
                    new("p11", Convert.ToString(user.Phonenumberconfirmed ? 1 : 0)),
                    new("p12", Convert.ToString(user.Twofactorenabled ? 1 : 0)),
                    new("p13", user.LockoutEnd == null ? DBNull.Value : user.LockoutEnd),
                    new("p14", Convert.ToString(user.LockoutEnable ? 1 : 0)),
                    new("p15", user.AccessFailedCount),

                }
            };

            await cmd.ExecuteNonQueryAsync();
        }


        public static async Task<ApplicationUser> FindByName(string name)
        {
            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand("SELECT * FROM applicationuser WHERE normalizedUserName = @p1 LIMIT 1;", conn)
            {
                Parameters =
                {
                    new("p1", name)
                }
            };

            var reader = await cmd.ExecuteReaderAsync();

            ApplicationUser user = new ApplicationUser(); 
            while (await reader.ReadAsync())
            {
                //var test = reader["lockoutend"] == DBNull.Value ? null : reader["lockoutend"];
                //DateTimeOffset test2 = (DateTime)reader["lockoutend"];
                user = new ApplicationUser(
                    (int)reader["id"],
                    (string)reader["username"],
                    (string)reader["normalizedusername"],
                    (string)reader["email"],
                    (string)reader["normalizedemail"],
                    (bool)reader["emailconfirmed"],
                    (string)reader["passwordhash"],
                    reader["securitystamp"] == DBNull.Value ? null : (string)reader["securitystamp"],
                    reader["concurrencystamp"] == DBNull.Value ? null : (string)reader["concurrencystamp"],
                    reader["phonenumber"] == DBNull.Value ? null : (string)reader["phonenumber"],
                    (bool)reader["phonenumberconfirmed"],
                    (bool)reader["twofactorenabled"],
                    reader["lockoutend"] == DBNull.Value ? null : (DateTime)reader["lockoutend"], //Needs to be converted to DateTime before converting to DateTimeOffset
                    (bool)reader["lockoutenable"],
                    (int)reader["accessfailedcount"]
                    );
            }


            return user;
        }

        public static async Task<ApplicationUser> FindByEmail(string email)
        {
            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand("SELECT * FROM applicationuser WHERE normalizedemail = @p1 LIMIT 1;", conn)
            {
                Parameters =
                {
                    new("p1", email)
                }
            };

            var reader = await cmd.ExecuteReaderAsync();

            ApplicationUser user = new ApplicationUser();
            while (await reader.ReadAsync())
            {
                //var test = reader["lockoutend"] == DBNull.Value ? null : reader["lockoutend"];
                //DateTimeOffset test2 = (DateTime)reader["lockoutend"];
                user = new ApplicationUser(
                    (int)reader["id"],
                    (string)reader["username"],
                    (string)reader["normalizedusername"],
                    (string)reader["email"],
                    (string)reader["normalizedemail"],
                    (bool)reader["emailconfirmed"],
                    (string)reader["passwordhash"],
                    reader["securitystamp"] == DBNull.Value ? null : (string)reader["securitystamp"],
                    reader["concurrencystamp"] == DBNull.Value ? null : (string)reader["concurrencystamp"],
                    reader["phonenumber"] == DBNull.Value ? null : (string)reader["phonenumber"],
                    (bool)reader["phonenumberconfirmed"],
                    (bool)reader["twofactorenabled"],
                    reader["lockoutend"] == DBNull.Value ? null : (DateTime)reader["lockoutend"], //Needs to be converted to DateTime before converting to DateTimeOffset
                    (bool)reader["lockoutenable"],
                    (int)reader["accessfailedcount"]
                    );
            }


            return user;
        }

        public static async Task<ApplicationUser> FindById(string id)
        {
            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand("SELECT * FROM applicationuser WHERE id = @p1 LIMIT 1;", conn)
            {
                Parameters =
                {
                    new("p1", Convert.ToInt32(id))
                }
            };

            var reader = await cmd.ExecuteReaderAsync();

            ApplicationUser user = new ApplicationUser();
            while (await reader.ReadAsync())
            {
                //var test = reader["lockoutend"] == DBNull.Value ? null : reader["lockoutend"];
                //DateTimeOffset test2 = (DateTime)reader["lockoutend"];
                user = new ApplicationUser(
                    (int)reader["id"],
                    (string)reader["username"],
                    (string)reader["normalizedusername"],
                    (string)reader["email"],
                    (string)reader["normalizedemail"],
                    (bool)reader["emailconfirmed"],
                    (string)reader["passwordhash"],
                    reader["securitystamp"] == DBNull.Value ? null : (string)reader["securitystamp"],
                    reader["concurrencystamp"] == DBNull.Value ? null : (string)reader["concurrencystamp"],
                    reader["phonenumber"] == DBNull.Value ? null : (string)reader["phonenumber"],
                    (bool)reader["phonenumberconfirmed"],
                    (bool)reader["twofactorenabled"],
                    reader["lockoutend"] == DBNull.Value ? null : (DateTime)reader["lockoutend"], //Needs to be converted to DateTime before converting to DateTimeOffset
                    (bool)reader["lockoutenable"],
                    (int)reader["accessfailedcount"]
                    );
            }


            return user;
        }

        public static async void UpdateUser(ApplicationUser user)
        {
            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand("UPDATE public.applicationuser " +
                "SET username=@p2, normalizedusername=@p3, email=@p4, normalizedemail=@p5, emailconfirmed=CAST(@p6 AS BIT), passwordhash=@p7, " +
                "securitystamp=@p8, concurrencystamp=@p9, phonenumber=@p10, phonenumberconfirmed=CAST(@p11 AS BIT), twofactorenabled=CAST(@p12 AS BIT), lockoutend=@p13, " +
                "lockoutenable=CAST(@p14 AS BIT), accessfailedcount=@p15 WHERE id = @p1; ", conn)
            {
                
                Parameters =
                {
                    new("p1", user.Id),
                    new("p2", user.Username),
                    new("p3", user.Normalizedusername),
                    new("p4", user.Email),
                    new("p5", user.Normalizedemail),
                    new("p6", Convert.ToString(user.Emailconfirmed ? 1 : 0)),
                    new("p7", user.Passwordhash),
                    new("p8", user.SecurityStamp == null ? DBNull.Value : user.SecurityStamp),
                    new("p9", user.ConcurrencyStamp == null ? DBNull.Value : user.ConcurrencyStamp),
                    new("p10", user.Phonenumber == null ? DBNull.Value : user.Phonenumber),
                    new("p11", Convert.ToString(user.Phonenumberconfirmed ? 1 : 0)),
                    new("p12", Convert.ToString(user.Twofactorenabled ? 1 : 0)),
                    new("p13", user.LockoutEnd == null ? DBNull.Value : user.LockoutEnd),
                    new("p14", Convert.ToString(user.LockoutEnable ? 1 : 0)),
                    new("p15", user.AccessFailedCount),

                }
            };

            await cmd.ExecuteNonQueryAsync();
        }

        #endregion

        #region Role queries

        #endregion
    }
}
