using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Diagnostics;
using ConsoleBaggageHandlingSystem;

namespace CreateFlightScheduleAndReservations
{
    public class FileCreater
    {
        static List<FlightSchedule> FlightScheduleList = new List<FlightSchedule>();

        static List<Reservation> ReservationsList = new List<Reservation>();

        static void Main(string[] args)
        {
            CreateBothFiles();
        }

        public static void CreateBothFiles()
        {
            CreateFlightSchedule();
            CreateReservation();
            CreateExtraFlightSchedule();
            CreateFSFile();
            CreateRFile();
        }


        static void WriteToLog(string input, string fileName)
        {
            try
            {
                // file path
                string filepath = $@"{fileName}.csv";

                // create stream writer true so its not overwriting file
                StreamWriter streamwrite = new StreamWriter(filepath, true);
                // add some lines
                streamwrite.WriteLine(input);
                // clear buffer
                streamwrite.Flush();

                // close file and streamwriter
                streamwrite.Close();

            }
            catch
            {
                Console.WriteLine("We should never end up here, but sometimes we do.");
            }



        }

        static void CreateExtraFlightSchedule()
        {
            //add 5 minutes so it can run 
            DateTime begin = FlightScheduleList[FlightScheduleList.Count - 1].Departure;


                //TODO: could be funny to random gatenum
                FlightScheduleList.Add(new FlightSchedule("LON", 1, begin.AddSeconds(1), begin.AddSeconds(10)));
                FlightScheduleList.Add(new FlightSchedule("STO", 2, begin.AddSeconds(1), begin.AddSeconds(10)));
                FlightScheduleList.Add(new FlightSchedule("CPH", 3, begin.AddSeconds(1), begin.AddSeconds(10)));
        }


        static void CreateFlightSchedule()
        {
            //add 5 minutes so it can run 
            DateTime now = DateTime.Now;

            //create 10 flightschedules
            for (int i = 1; i <= 10; i++)
            {
                DateTime begin = now.AddSeconds((i*10)+1);
                //TODO: could be funny to random gatenum
                FlightScheduleList.Add(new FlightSchedule("LON", 1, begin, begin.AddSeconds(10)));
                FlightScheduleList.Add(new FlightSchedule("STO", 2, begin, begin.AddSeconds(10)));
                FlightScheduleList.Add(new FlightSchedule("CPH", 3, begin, begin.AddSeconds(10)));
            }
        }
        static void CreateReservation()
        {
            int counter = 0;
            foreach (var item in FlightScheduleList)
            {
                for (int i = 1; i <= 22; i++)
                {
                    ReservationsList.Add( new Reservation(counter, $"Passenger{counter}", item));
                    counter++;
                }
            }
        }

        static void CreateFSFile()
        {
            //if file exist then delete it
            if (File.Exists($@"FlightSchedules.csv"))
            {
                File.Delete($@"FlightSchedules.csv");
            }
            WriteToLog("Destination;GateNum;Arrival;Departure", "FlightSchedules");
            foreach (var item in FlightScheduleList)
            {
                WriteToLog($"{item.Destination};{item.GateNum};{item.Arrival};{item.Departure}", "FlightSchedules");
            }
        }

        static void CreateRFile()
        {
            //if file exist then delete it
            if (File.Exists($@"Reservation.csv"))
            {
                File.Delete($@"Reservation.csv");
            }
            WriteToLog("PassengerNumber;Name;Destination;GateNum;Arrival;Departure", "Reservation");
            foreach (var item in ReservationsList)
            {
                WriteToLog($"{item.PassengerNumber};{item.Name};{item.Destination};{item.Schedule.GateNum};{item.Schedule.Arrival};{item.Schedule.Departure}", "Reservation");
            }
        }


    }
}
