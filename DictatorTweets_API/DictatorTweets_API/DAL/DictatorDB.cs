using System.Collections.Generic;
using System.Security.Policy;
using System.Threading.Tasks;
using System;
using Npgsql;
using DictatorTweets_API.Models;

namespace DictatorTweets_API.DAL
{
    public class DictatorDB : IDictatorDB
    {
        private static string connectionString = $"Host={Environment.GetEnvironmentVariable("DB_HOST")};" +
            $"Username={Environment.GetEnvironmentVariable("DB_USERNAME")};" +
            $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")};" +
            $"Database={Environment.GetEnvironmentVariable("DB_DATABASE")};" +
            $"Port={Environment.GetEnvironmentVariable("DB_PORT")}";

        public async Task<Dictator> CreateDictator(Dictator dictator)
        {
            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand("SELECT * FROM DictatorTweets.func_insert_dictator(@p1, @p2, @p3, @p4, @p5);", conn)
            {
                Parameters =
                {
                    new("p1", dictator.FirstName),
                    new("p2", dictator.LastName),
                    new("p3", dictator.BirthYear),
                    new("p4", dictator.DeadYear),
                    new("p5", dictator.ShortDescription),

                }
            };

            var reader = await cmd.ExecuteReaderAsync();
            Dictator newdictator = null;
            while (await reader.ReadAsync())
            {
                newdictator= new Dictator(
                    (int)reader["id"],
                    (string)reader["firstname"],
                    (string)reader["lastname"],
                    (int)reader["birthyear"],
                    (int)reader["deadyear"],
                    (string)reader["shortDesciption"],
                    reader["mappningtext"] == DBNull.Value ? null : (string)reader["mappningtext"]);
            }

            return newdictator;
        }

        public async void UpdateDictator(Dictator dictator)
        {
            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand("UPDATE DictatorTweets.Dictators SET firstname = @p1, lastname = @p2, birthyear = @p3, deadyear = @p4, shortdesciption = @p5 WHERE id = @p6;", conn)
            {
                Parameters =
                {
                    new("p1", dictator.FirstName),
                    new("p2", dictator.LastName),
                    new("p3", dictator.BirthYear),
                    new("p4", dictator.DeadYear),
                    new("p5", dictator.ShortDescription),
                    new("p6", dictator.Id)

                }
            };

            await cmd.ExecuteNonQueryAsync();
        }

        public async void DeleteDictator(Dictator dictator)
        {
            // TODO: Create procedure to add mapping text again 
            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand("CALL DictatorTweets.proc_delete_dictator(@p1);", conn)
            {
                Parameters =
                {
                    new("p1", dictator.Id)
                }
            };

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<List<Dictator>> GetDictators()
        {
            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand("SELECT * FROM DictatorTweets.Dictators;", conn);

            var reader = await cmd.ExecuteReaderAsync();
            List<Dictator> dictators = new List<Dictator>();
            while (await reader.ReadAsync())
            {
                dictators.Add(new Dictator(
                    (int)reader["id"],
                    (string)reader["firstname"],
                    (string)reader["lastname"],
                    (int)reader["birthyear"],
                    (int)reader["deadyear"],
                    (string)reader["shortDesciption"],
                    reader["mappningtext"] == DBNull.Value ? null : (string)reader["mappningtext"]));
            }

            return dictators;
        }


    }
}
