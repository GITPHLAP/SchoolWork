using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace BaggageHandlingSystem
{
    class SimulationManager
    {
        //THIS COULD MAYBE BE CENTRAL SERVER
        //TODO: ReservationSystem
        //TODO: FlightSchedule
        //TODO: Desks
        //TODO: SortingSystem
        //TODO: Gates
        //TODO: Simulator Manager Klasse
        //TODO: Build Central server

        //Central Thread wait lock
        public static object CentralLock = new object();

        //Destination string list
        //public static List<string> Destinations = new List<string>();

        //List of gates 
        public static List<Gate> Gates = new List<Gate>();

        //list of desks 
        List<Desk> desks = new List<Desk>();


        //list with flightplans
        public static List<FlightSchedule> Flightplans = new List<FlightSchedule>();


        //void CentralServerMethod()
        //{
        //    while (true)
        //    {

        //    }
        //}


        void OpenDesksAndGates()
        {
            lock (CentralLock)
            {
                Monitor.PulseAll(CentralLock);
            }
        }

        //TODO: Implement Method to add Flightschedule to program
        void NewFlightSchedule()
        {

        }

        public void StartSimulation()
        {



            Flightplans.Add(new FlightSchedule("LON", 1, DateTime.Now, DateTime.Now.AddMinutes(2)));
            Flightplans.Add(new FlightSchedule("STO", 2, DateTime.Now, DateTime.Now.AddMinutes(1)));
            Flightplans.Add(new FlightSchedule("CPH", 3, DateTime.Now, DateTime.Now.AddMinutes(2)));

            Flightplans.Add(new FlightSchedule("LON", 3, DateTime.Now.AddMinutes(3), DateTime.Now.AddMinutes(6)));
            Flightplans.Add(new FlightSchedule("STO", 1, DateTime.Now.AddMinutes(3), DateTime.Now.AddMinutes(7)));
            Flightplans.Add(new FlightSchedule("CPH", 2, DateTime.Now.AddMinutes(3), DateTime.Now.AddMinutes(8)));



            //add gates
            Gates.Add(new Gate(1, 20));
            Gates.Add(new Gate(2, 20));
            Gates.Add(new Gate(3, 20));

            desks.Add(new Desk("Desk1"));
            desks.Add(new Desk("Desk2"));

            SortingSystem sortingSystem = new SortingSystem();

            Thread sortT = new Thread(sortingSystem.SplitterMethod);
            sortT.Name = "SortingThread";
            sortT.Priority = ThreadPriority.Highest;




            desks[0].StartDesk();
            desks[1].StartDesk();

            Thread.Sleep(500);

            foreach (var item in Gates)
            {
                item.StartGate();
            }
            Thread.Sleep(1000);
            //start all gate threads

            sortT.Start();
        }


        public void GUI()
        {
            //variable to deskname so all switch can use it
            string desknameinput;

            Console.WriteLine("Start simulation press 1");
            Console.WriteLine("Exit program press 0");

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

            Console.Clear();

            while (true)
            {

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

                        desks.Add(new Desk(desknameinput));

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
                Console.WriteLine("Desk lukket");
            }
            else
                Console.WriteLine("Der var ingen desk med navnet");
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

        void ShowFlightPlan()
        {
            foreach (var item in Flightplans)
            {
                Console.WriteLine("{0} Done? status: {1}", item.Destination, item.IsDone);
            }
        }

    }
}