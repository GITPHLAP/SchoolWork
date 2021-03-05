using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;


namespace BaggageHandlingSystem
{
    public class ReservationSystem
    {
        static List<Reservation> Reservations = new List<Reservation>();


        //TODO: split this method... RememberSOLID
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
                    Reservations.Add(new Reservation(Convert.ToInt32(values[0]),
                        values[1],
                        FindFlightSchedule(
                            values[2],
                            Convert.ToInt32(values[3]),
                            Convert.ToDateTime(values[4].ToString()),
                            Convert.ToDateTime(values[5].ToString()))
                        ));

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


        FlightSchedule FindFlightSchedule(string destination, int gateNum, DateTime arrival, DateTime departure)
        {
            FlightSchedule scheduleToCheck = new FlightSchedule(destination, gateNum, arrival, departure);

            //is there any schedules equals the line
            bool dotheyMatch = SimulationManager.Flightplans.Any(fp => fp.Equals(scheduleToCheck));
            if (!dotheyMatch)
            {
                //if no then add it to FlightSchedule
                SimulationManager.Flightplans.Add(scheduleToCheck);
            }
            return SimulationManager.Flightplans.Where(fp => fp.Equals(scheduleToCheck)).First();
        }


    }
}
