using System;
using System.IO;

namespace MyDotEnv
{
    public class DotEnv
    {
        /// <summary>
        /// Load all the variables into enviroment variables. <br>
        /// Use variable: Environment.GetEnvironmentVariable("[VARIABLE_NAME]")
        /// </summary>
        /// <param name="filePath"> Path.Combine(Directory.GetCurrentDirectory(), ".env"</param>
        public static void Load(string filePath)
        {
            if (!File.Exists(filePath))
                return;

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split(
                    '=',
                    StringSplitOptions.RemoveEmptyEntries);

                Environment.SetEnvironmentVariable(parts[0], parts[1]);
            }
        }
    }
}
