using Npgsql;
using SecureLoginWebAPI.Models;
using System;
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
            
            await using var cmd = new NpgsqlCommand("INSERT INTO applicationuser VALUES(@p1, @p2, @p3, @p4, @p5, CAST(@p6 AS BIT), @p7, @p8, CAST(@p9 AS BIT), CAST(@p10 AS BIT))", conn)
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
                    new("p8", user.Phonenumber == null ? DBNull.Value : user.Phonenumber),
                    new("p9", Convert.ToString(user.Phonenumberconfirmed ? 1 : 0)),
                    new("p10", Convert.ToString(user.Twofactorenabled ? 1 : 0)),

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
                user = new ApplicationUser(
                    (int)reader["id"],
                    (string)reader["username"],
                    (string)reader["normalizedusername"],
                    (string)reader["email"],
                    (string)reader["normalizedemail"],
                    (bool)reader["emailconfirmed"],
                    (string)reader["passwordhash"],
                    reader["phonenumber"] == DBNull.Value ? null : (string)reader["phonenumber"],
                    (bool)reader["phonenumberconfirmed"],
                    (bool)reader["twofactorenabled"]
                    );
            }


            return user;
        }


        #endregion

        #region Role queries

        #endregion
    }
}
