using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace ConsoleBaggageHandlingSystem
{
    public class FlightScheduleImporter
    {
        //string distination, int gateNum, DateTime arrival, DateTime departure

        //TODO: split this method... RememberSOLID Look also in reservation System
        public void ReadCSVFileAndAddToList(string filepath)
        {

            // Open file with Filestream
            FileStream streamfile = new FileStream(filepath, FileMode.Open, FileAccess.Read);

            StreamReader streamreader = new StreamReader(streamfile);

            bool IsFirstLine = true;

            while (!streamreader.EndOfStream)
            {
                string readerline = streamreader.ReadLine();

                if (!IsFirstLine)
                {
                    //split the line into arrays
                    string[] values = readerline.Split(';');

                    //add new reservatio to list and create a new reservation and FlightSchedule
                    SimulationManager.Flightplans.Add(new FlightSchedule(values[0],
                        Convert.ToInt32(values[1]),
                            Convert.ToDateTime(values[2].ToString()),
                            Convert.ToDateTime(values[3].ToString()))
                        );

                }
                else
                {
                    IsFirstLine = false;
                }


            }

            //Close streamreader and file stream
            streamreader.Close();

            streamfile.Close();

        }

    }
}
