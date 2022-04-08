using Npgsql;
using SecureLoginWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecureLoginWebAPI
{
    public class Postgres
    {

        static string connectionString = $"Host={Environment.GetEnvironmentVariable("DB_HOST")};" +
            $"Username={Environment.GetEnvironmentVariable("DB_USERNAME")};" +
            $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")};" +
            $"Database={Environment.GetEnvironmentVariable("DB_DATABASE")};" +
            $"Port={Environment.GetEnvironmentVariable("DB_PORT")}";

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

            await using var cmd = new NpgsqlCommand("CALL public.proc_insert_applicationuser(@p2, @p3, @p4, @p5, CAST(@p6 AS BIT), @p7," +
                " @p8, @p9, @p10, CAST(@p11 AS BIT), CAST(@p12 AS BIT), @p13, CAST(@p14 AS BIT), @p15, false, @p17);", conn)
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
                    new("p17", user.AuthenticatorKey == null ? DBNull.Value : user.AuthenticatorKey ),

                }
            };

            await cmd.ExecuteNonQueryAsync();
        }


        public static async Task<ApplicationUser> FindByName(string name)
        {
            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand("SELECT * FROM public.func_find_user_by_name(@p1);", conn)
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
                    (int)reader["_id"],
                    (string)reader["_username"],
                    (string)reader["_normalizedusername"],
                    (string)reader["_email"],
                    (string)reader["_normalizedemail"],
                    (bool)reader["_emailconfirmed"],
                    (string)reader["_passwordhash"],
                    reader["_securitystamp"] == DBNull.Value ? null : (string)reader["_securitystamp"],
                    reader["_concurrencystamp"] == DBNull.Value ? null : (string)reader["_concurrencystamp"],
                    reader["_phonenumber"] == DBNull.Value ? null : (string)reader["_phonenumber"],
                    (bool)reader["_phonenumberconfirmed"],
                    (bool)reader["_twofactorenabled"],
                    reader["_lockoutend"] == DBNull.Value ? null : (DateTime)reader["_lockoutend"], //Needs to be converted to DateTime before converting to DateTimeOffset
                    (bool)reader["_lockoutenable"],
                    (int)reader["_accessfailedcount"],
                    (bool)reader["_deleted"],
                    reader["_authenticatorkey"] == DBNull.Value ? null : (string)reader["_authenticatorkey"]
                    );
            }

            user.RecoveryCodes = await FindRecoveryCodesById(user.Id) ?? new List<RecoverCode>();

            return user;
        }

        public static async Task<ApplicationUser> FindByEmail(string email)
        {
            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand("SELECT * FROM public.func_find_user_by_email(@p1);", conn)
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
                    (int)reader["_id"],
                    (string)reader["_username"],
                    (string)reader["_normalizedusername"],
                    (string)reader["_email"],
                    (string)reader["_normalizedemail"],
                    (bool)reader["_emailconfirmed"],
                    (string)reader["_passwordhash"],
                    reader["_securitystamp"] == DBNull.Value ? null : (string)reader["_securitystamp"],
                    reader["_concurrencystamp"] == DBNull.Value ? null : (string)reader["_concurrencystamp"],
                    reader["_phonenumber"] == DBNull.Value ? null : (string)reader["_phonenumber"],
                    (bool)reader["_phonenumberconfirmed"],
                    (bool)reader["_twofactorenabled"],
                    reader["_lockoutend"] == DBNull.Value ? null : (DateTime)reader["_lockoutend"], //Needs to be converted to DateTime before converting to DateTimeOffset
                    (bool)reader["_lockoutenable"],
                    (int)reader["_accessfailedcount"],
                    (bool)reader["_deleted"],
                    reader["_authenticatorkey"] == DBNull.Value ? null : (string)reader["_authenticatorkey"]
                    );
            }

            user.RecoveryCodes = await FindRecoveryCodesById(user.Id) ?? new List<RecoverCode>();


            return user;
        }

        public static async Task<ApplicationUser> FindById(string id)
        {
            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand("SELECT * FROM public.func_find_user_by_id(@p1);", conn)
            {
                Parameters =
                {
                    new("p1", Convert.ToInt32(id))
                }
            };

            var reader = await cmd.ExecuteReaderAsync();

            ApplicationUser user = null;
            while (await reader.ReadAsync())
            {
                user = new ApplicationUser(
                    (int)reader["_id"],
                    (string)reader["_username"],
                    (string)reader["_normalizedusername"],
                    (string)reader["_email"],
                    (string)reader["_normalizedemail"],
                    (bool)reader["_emailconfirmed"],
                    (string)reader["_passwordhash"],
                    reader["_securitystamp"] == DBNull.Value ? null : (string)reader["_securitystamp"],
                    reader["_concurrencystamp"] == DBNull.Value ? null : (string)reader["_concurrencystamp"],
                    reader["_phonenumber"] == DBNull.Value ? null : (string)reader["_phonenumber"],
                    (bool)reader["_phonenumberconfirmed"],
                    (bool)reader["_twofactorenabled"],
                    reader["_lockoutend"] == DBNull.Value ? null : (DateTime)reader["_lockoutend"], //Needs to be converted to DateTime before converting to DateTimeOffset
                    (bool)reader["_lockoutenable"],
                    (int)reader["_accessfailedcount"],
                    (bool)reader["_deleted"],
                    reader["_authenticatorkey"] == DBNull.Value ? null : (string)reader["_authenticatorkey"]
                    );
            }

            //user.UserLogins = await FindUserLoginById(user.Id);
            user.RecoveryCodes = await FindRecoveryCodesById(user.Id) ?? new List<RecoverCode>();

            return user;
        }

        private static async Task<List<RecoverCode>> FindRecoveryCodesById(int id)
        {
            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand("SELECT * FROM public.func_get_recoverycodes(@p1);", conn)
            {
                Parameters =
                {
                    new("p1", id)
                }
            };

            var reader = await cmd.ExecuteReaderAsync();

            List<RecoverCode> codes = new();
            while (await reader.ReadAsync())
            {
                codes.Add(new RecoverCode(
                    (string)reader["_code"],
                    (bool)reader["_redeemed"]
                    ));
            }


            return codes;
        }
        public static async void UpdateUser(ApplicationUser user)
        {
            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand("CALL public.proc_update_applicationuser(@p1, @p2, @p3, @p4, @p5, CAST(@p6 AS BIT), @p7," +
                " @p8, @p9, @p10, CAST(@p11 AS BIT), CAST(@p12 AS BIT), @p13, CAST(@p14 AS BIT), @p15, @p16, @p17);", conn)
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
                    new("p16", user.Deleted),
                    new("p17", user.AuthenticatorKey == null ? DBNull.Value : user.AuthenticatorKey ),

                }
            };

            await cmd.ExecuteNonQueryAsync();

            if (user.RecoveryCodes != null)
            {
                UpdateRecCodes(user.RecoveryCodes, user.Id);
            }
        }

        private static async void UpdateRecCodes(IList<RecoverCode> rc, int userid)
        {
            DeleteRecCodes(userid);

            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();
            foreach (RecoverCode item in rc)
            {
                await using var cmd = new NpgsqlCommand("CALL public.proc_update_recoverycodes(@p1, @p2, @p3);", conn)
                {

                    Parameters =
                {
                    new("p1", userid),
                    new("p2", item.Code),
                    new("p3", item.Redeemed),
                }
                };

                await cmd.ExecuteNonQueryAsync();
            }

        }

        private static async void DeleteRecCodes(int userid)
        {
            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();
            await using var cmd = new NpgsqlCommand("DELETE FROM recoverycodes WHERE userid = @p1;", conn)
            {

                Parameters =
                {
                    new("p1", userid)
                }
            };

            await cmd.ExecuteNonQueryAsync();

        }

        public static async void DeleteUser(int userid)
        {
            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand("CALL public.proc_delete_applicationuser(@p1);", conn)
            {

                Parameters =
                {
                    new("p1", userid)

                }
            };

            await cmd.ExecuteNonQueryAsync();
        }
        #endregion

        #region Role queries

        #endregion
    }
}
