using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ConsoleBaggageHandlingSystem
{
    public class SimulationManager
    {
        //TODO: Write Better Logging output and add Timestamps

        //All those is static because its the same 
        //TODO: There should only be initilaize one of SimulationManager
        //Central lock to Thread wait when No More Schedules
        public static object CentralLock = new object();

        //List of gates 
        static List<Gate> gates = new List<Gate>();

        //list of desks 
        public static List<Desk> Desks = new List<Desk>();

        //Create a sorting system 
        SortingSystem sortingSystem = new SortingSystem();

        //list with flightplans
        public static List<FlightSchedule> Flightplans = new List<FlightSchedule>();

        //Variable is used to show a message to console.
        public static bool NoMoreFlightSchedules;

        public SortingSystem SortingSystem { get => sortingSystem; set => sortingSystem = value; }
        public static  List<Gate> Gates { get => gates; set => gates = value; }

        //Event handler to Flight status Departure and arrival and Luggage count
        public event EventHandler UpdateGates;


        #region CLI related Methods
        public void CLI()
        {
            //more global variable for deskname so all switch cases can use it
            string desknameinput;

            Console.WriteLine("Start simulation press 1");
            Console.WriteLine("Exit program press 0");

            //var to userinput to switch-case
            char userInput = Console.ReadKey().KeyChar;
            switch (userInput)
            {
                case '0':
                    Environment.Exit(1);
                    break;
                case '1':
                    StartSimulation();
                    break;
                default:
                    break;
            }

            //Clear console
            Console.Clear();

            while (true)
            {
                //then show a red line 
                if (NoMoreFlightSchedules)
                {
                    ShowNoMoreFlightSchedules();
                }
                Console.WriteLine("Open new desk press 1");
                Console.WriteLine("Show List of desk and Gate status press 2");
                Console.WriteLine("Close desk press 3");
                Console.WriteLine("Show flightplan press 4");
                Console.WriteLine("Start gates and desks press 5");



                userInput = Console.ReadKey().KeyChar;
                switch (userInput)
                {
                    case '0':
                        Environment.Exit(1);
                        break;
                    case '1':
                        Console.WriteLine("---------------------");
                        Console.WriteLine("This is a list of desks");
                        ShowDesks();
                        Console.WriteLine("---------------------");
                        Console.WriteLine("Write name on new desk and press enter");
                        Console.WriteLine("DONT write the same name");

                        desknameinput = Console.ReadLine();

                        //add the desk to the desks list
                        Desks.Add(new Desk(desknameinput));

                        //start the desk-Thread we just add to list
                        GetDeskFromName(desknameinput).StartDesk();

                        break;
                    case '2':
                        Console.WriteLine("---------------------");
                        Console.WriteLine("This is a list of desks");
                        ShowDeskStatus();
                        Console.WriteLine("---------------------");
                        Console.WriteLine("This is a list of desks");
                        ShowGateStatus();
                        Console.WriteLine("---------------------");
                        break;
                    case '3':
                        Console.WriteLine("---------------------");
                        Console.WriteLine("This is a list of open desks");
                        ShowOpenDesks();
                        Console.WriteLine("---------------------");
                        Console.WriteLine("Write name on desk and press enter");

                        desknameinput = Console.ReadLine();
                        CloseDesk(desknameinput);
                        break;
                    case '4':
                        Console.WriteLine("---------------------");
                        Console.WriteLine("This is a list of flightplans");
                        ShowFlightPlan();
                        Console.WriteLine("---------------------");
                        break;
                    case '5':
                        OpenDesksAndGates();
                        break;
                    default:
                        break;
                }
                Console.ReadLine();

                Console.Clear();
            }

        }

        public void ShowDeskStatus()
        {
            foreach (var item in Desks)
            {
                Console.WriteLine(item.DeskT.ThreadState);
            }
        }
        public void ShowGateStatus()
        {
            foreach (var item in Gates)
            {
                Console.WriteLine(item.gateT.ThreadState);
            }
        }

        public void CloseDesk(string deskname)
        {

            if (GetDeskFromName(deskname) != null)
            {
                GetDeskFromName(deskname).CloseDesk();
                Console.WriteLine("Desk sat til at lukke");
            }
            else
                Console.WriteLine("Der var ingen desk med navnet");
        }

        public void ShowFlightPlan()
        {
            foreach (var item in Flightplans)
            {
                Console.WriteLine("FlightSchedule to {0} time: {1} - {2} Done? status: {3}"
                    , item.Destination
                    , item.Arrival
                    , item.Departure
                    , item.IsDone
                    );
            }
        }

        public void ShowNoMoreFlightSchedules()
        {
            Console.BackgroundColor = ConsoleColor.Red;

            Console.WriteLine("No scheduled flightschedules");

            Console.BackgroundColor = ConsoleColor.Black;
        }

        #endregion

        public void StartSimulation()
        {
            //This is to read Reservations and Flightschedules and add it too a list 
            FlightScheduleImporter importFS = new FlightScheduleImporter();

            importFS.ReadCSVFileAndAddToList(@"FlightSchedules.csv");

            ReservationSystem importreservation = new ReservationSystem();

            importreservation.ReadCSVFileAndAddToList(@"Reservation.csv");



            #region Add FlightSchedules
            //Flightplans.Add(new FlightSchedule("LON", 1, DateTime.Now, DateTime.Now.AddMinutes(2)));
            //Flightplans.Add(new FlightSchedule("STO", 2, DateTime.Now, DateTime.Now.AddMinutes(1)));
            //Flightplans.Add(new FlightSchedule("CPH", 3, DateTime.Now, DateTime.Now.AddMinutes(2)));

            //Flightplans.Add(new FlightSchedule("LON", 3, DateTime.Now.AddMinutes(3), DateTime.Now.AddMinutes(6)));
            //Flightplans.Add(new FlightSchedule("STO", 1, DateTime.Now.AddMinutes(3), DateTime.Now.AddMinutes(7)));
            //Flightplans.Add(new FlightSchedule("CPH", 2, DateTime.Now.AddMinutes(3), DateTime.Now.AddMinutes(8)));
            #endregion

            //add gates to list 
            Gates.Add(new Gate(1, 20));
            Gates.Add(new Gate(2, 20));
            Gates.Add(new Gate(3, 20));

            //make a event on all gates.
            foreach (var item in Gates)
            {
                item.GatesFlightStatus += Gate_GateStatus;

                item.DepartureFlight += Gate_DepartureFlight;
            }

            //add desks to list
            Desks.Add(new Desk("Desk1"));
            Desks.Add(new Desk("Desk2"));



            //make sortingsystem event
            sortingSystem.UpdateGatesLuggage += SortingSystem_UpdateGatesLuggage;


            //Create a thread on SortingSystem
            Thread sortT = new Thread(sortingSystem.SplitterMethod);

            //Give the thread a name and priority
            sortT.Name = "SortingThread";
            sortT.Priority = ThreadPriority.Highest;

            //start gateThreads
            foreach (var item in Gates)
            {
                item.StartGate();
            }
            Thread.Sleep(1000);

            //start SortingSystem Thread
            sortT.Start();

            //start desk Threads
            Desks[0].StartDesk();
            Desks[1].StartDesk();
        }

        private void Gate_DepartureFlight(object sender, EventArgs e)
        {
            //casting event
            FlightEventArgs flightevent = (FlightEventArgs)e;

            //check the cast passed fine
            if (flightevent != null)
            {
                Logging.WriteToLog($"[Manager EVENT] Flight from " +
                $"{flightevent.Schedule.Destination} " +
                $"kl. {flightevent.Schedule.Departure.ToString("dd-MM-yyyy HH:mm:ss")} " +
                $"has departured with {flightevent.Schedule.PassengerAmount}");
            }

        }

        private void SortingSystem_UpdateGatesLuggage(object sender, EventArgs e)
        {
            Logging.WriteToLog("[Manager EVENT] Send GateStatusUpdate about luggage");
            UpdateGates?.Invoke(sender, EventArgs.Empty);
        }

        private void Gate_GateStatus(object sender, EventArgs e)
        {
            Logging.WriteToLog("[Manager EVENT] Manager Send GateStatusUpdate about all status ");
            UpdateGates?.Invoke(sender, EventArgs.Empty);
        }

        public void OpenDesksAndGates()
        {
            lock (CentralLock)
            {
                Monitor.PulseAll(CentralLock);
            }
        }

        void NewReservations()
        {
            //TODO: Implement Method to add Reservations to program

            //NoMoreFlightSchedules = false;
        }

        public Desk GetDeskFromName(string deskname)
        {
            return Desks.Where(d => d.DeskName == deskname).FirstOrDefault();
        }

        public void ShowDesks()
        {
            foreach (var item in Desks)
            {
                Console.WriteLine(item.DeskName);
            }
        }

        public void ShowOpenDesks()
        {
            foreach (var item in Desks.Where(d => d.DeskT.IsAlive))
            {
                Console.WriteLine(item.DeskName);
            }
        }

    }
}