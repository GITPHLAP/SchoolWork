using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Diagnostics;


namespace BaggageHandlingSystem
{
    class Logging
    {

        static object locks = new object();

public static void WriteToLog(string input)
        {
            Debug.WriteLine(input);

            try
            {
                lock (locks)
                {
                    string datestr = DateTime.Now.ToString("yyyy-MM-dd");

                    // file path
                    string filepath = $@"LogFile{datestr}.txt";

                    // create stream writer true so its not overwriting file
                    StreamWriter streamwrite = new StreamWriter(filepath, true);
                    // add some lines
                    streamwrite.WriteLine(input);
                    // clear buffer
                    streamwrite.Flush();

                    // close file and streamwriter
                    streamwrite.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("We should never end up here, but sometimes we do.");
            }

            
            
        }

    }
}
