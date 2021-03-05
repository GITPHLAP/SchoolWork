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
        public static List<string> Destinations = new List<string>();

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

        void NewFlightSchedule()
        {

        }

        public void StartSimulation()
        {
            //Thread centralThread = new Thread(CentralServerMethod);
            //centralThread.Name = "CentralThread";




            Flightplans.Add(new FlightSchedule("LON", 1, DateTime.Now, DateTime.Now.AddMinutes(2)));
            Flightplans.Add(new FlightSchedule("STO", 2, DateTime.Now, DateTime.Now.AddMinutes(1)));
            Flightplans.Add(new FlightSchedule("CPH", 3, DateTime.Now, DateTime.Now.AddMinutes(2)));

            Flightplans.Add(new FlightSchedule("LON", 3, DateTime.Now.AddMinutes(5), DateTime.Now.AddMinutes(7)));
            Flightplans.Add(new FlightSchedule("STO", 1, DateTime.Now.AddMinutes(5), DateTime.Now.AddMinutes(8)));
            Flightplans.Add(new FlightSchedule("CPH", 2, DateTime.Now.AddMinutes(5), DateTime.Now.AddMinutes(7)));



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
            //start all gate threads
            foreach (var item in Gates)
            {
                item.StartGate();
            }

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
            //Desk desk = GetDeskFromName(deskname);

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
                Console.WriteLine("{0} Done? status: {1}", item.Distination, item.IsDone);
            }
        }

        //#region Controller Methods
        //void ControllerMethod()
        //{
        //    while (true)
        //    {
        //        foreach (var item in Flightplans)
        //        {
        //            // and if its not already begin
        //            if (item.From <= DateTime.Now && !item.IsDone)
        //            {
        //                CheckInControl(item);
        //                item.IsDone = true;
        //            }
        //        }

        //        //if there is any there should take off
        //        if (Flightplans.Any(fp => fp.To >= DateTime.Now))
        //        {
        //            //temp to be sure is the same FlightPlan the mothod will touch
        //            FlightSchedule plan = Flightplans.Where(fp => fp.To >= DateTime.Now).First();
        //            //TODO: Could let user decide if gate should forced to close
        //            TimeToTakeOff(plan);
        //            Flightplans.Remove(plan);
        //        }
        //    }
        //}

        //void CheckInControl(FlightSchedule item)
        //{
        //    //we found the gate there match the flightplans gatenumber
        //    Gate thisGate = Gates.Where(g => g.GateNumber == item.GateNum).FirstOrDefault();

        //    //If gate is alive nothing happend
        //    //while (thisGate.gateT == null ? false : true)
        //    //{
        //    //    Debug.WriteLine($"{thisGate.gateT} is Alive");
        //    //}

        //    //Update departures list
        //    UpdateDepartures(thisGate.Destination, item.Departure);

        //    //We set the departure on the gate
        //    thisGate.Destination = item.Departure;


        //    //start gate
        //    thisGate.StartGate();
        //    Logging.WriteToLog($"{thisGate.gateT.Name} is opened");


        //    //if all desk is (null) not alive then wake them all
        //    if (desks.All(d => d.deskT == null))
        //    {
        //        foreach (var desk in desks)
        //        {
        //            desk.StartDesk();
        //            Logging.WriteToLog($"{desk.deskT.Name} is opened");
        //        }
        //    }

        //    //all gates have 10 seconds from this step.
        //    //add "to" date time which is from now and 10 sec
        //    item.To = DateTime.Now.AddSeconds(10);
        //}

        //void TimeToTakeOff(FlightSchedule item)
        //{
        //    //we found the gate there match the flightplans gatenumber
        //    Gate gateToClose = Gates.Where(g => g.GateNumber == item.GateNum).FirstOrDefault();


        //    if (gateToClose.gateT.IsAlive)
        //    {
        //        //If gate still working give them a second
        //        Thread.Sleep(1000);

        //        Logging.WriteToLog($"{gateToClose.gateT.Name} forced to close");

        //        //Update departures list
        //        UpdateDepartures(gateToClose.Destination, "");

        //        //We set the departure on the gate
        //        gateToClose.Destination = "";

        //        //close gate
        //        gateToClose.CloseGate();
        //        Logging.WriteToLog($"{gateToClose.gateT.Name} is closed");

        //    }
        //    else
        //    {
        //        //Update departures list
        //        UpdateDepartures(gateToClose.Destination, "");

        //        //We set the departure on the gate
        //        gateToClose.Destination = "";
        //    }

        //    //if all gates is (null) not alive then close all desks
        //    if (Gates.All(d => d.gateT == null))
        //    {
        //        foreach (var desk in desks)
        //        {
        //            desk.CloseDesk();
        //            Logging.WriteToLog($"{desk.deskT.Name} is closed");

        //        }
        //    }
        //}

        //void UpdateDepartures(string olddep, string newdep)
        //{
        //    departures.Remove(olddep);

        //    departures.Add(newdep);
        //}
        //#endregion
    }
}