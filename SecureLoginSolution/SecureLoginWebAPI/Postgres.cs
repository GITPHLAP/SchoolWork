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

    }
}
