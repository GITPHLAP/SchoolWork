using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BaggageHandlingSystem
{
    class SimulationManager
    {
        //TODO: Write Better Logging output and add Timestamps

        //Central lock to Thread wait when No More Schedules
        public static object CentralLock = new object();

        //List of gates 
        public static List<Gate> Gates = new List<Gate>();

        //list of desks 
        List<Desk> desks = new List<Desk>();

        //list with flightplans
        public static List<FlightSchedule> Flightplans = new List<FlightSchedule>();

        //Variable is used to show a message to console.
        public static bool NoMoreFlightSchedules;

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
                        desks.Add(new Desk(desknameinput));

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

        void ShowDeskStatus()
        {
            foreach (var item in desks)
            {
                Console.WriteLine(item.deskT.ThreadState);
            }
        }
        void ShowGateStatus()
        {
            foreach (var item in Gates)
            {
                Console.WriteLine(item.gateT.ThreadState);
            }
        }

        void CloseDesk(string deskname)
        {

            if (GetDeskFromName(deskname) != null)
            {
                GetDeskFromName(deskname).CloseDesk();
                Console.WriteLine("Desk sat til at lukke");
            }
            else
                Console.WriteLine("Der var ingen desk med navnet");
        }

        void ShowFlightPlan()
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

        void StartSimulation()
        {
            //This is to read Reservations and add it too a list 
            ReservationSystem testre = new ReservationSystem();

            testre.ReadCSVFileAndAddToList(@"Reservation.csv");

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
            
            //add desks to list
            desks.Add(new Desk("Desk1"));
            desks.Add(new Desk("Desk2"));

            //Create a sorting system 
            SortingSystem sortingSystem = new SortingSystem();
            
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
            desks[0].StartDesk();
            desks[1].StartDesk();
        }

        void OpenDesksAndGates()
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

        Desk GetDeskFromName(string deskname)
        {
            return desks.Where(d => d.DeskName == deskname).FirstOrDefault();
        }

        void ShowDesks()
        {
            foreach (var item in desks)
            {
                Console.WriteLine(item.DeskName);
            }
        }

        void ShowOpenDesks()
        {
            foreach (var item in desks.Where(d => d.deskT.IsAlive))
            {
                Console.WriteLine(item.DeskName);
            }
        }

    }
}