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
        //TODO: ReservationSystem
        //TODO: FlightSchedule
        //TODO: Desks
        //TODO: SortingSystem
        //TODO: Gates
        //TODO: Simulator Manager Klasse


        //Destination string list
        public static List<string> departures = new List<string>();

        //List of gates 
        public static List<Gate> Gates = new List<Gate>();

        //list of desks 
        List<Desk> desks = new List<Desk>();


        //list with flightplans
        List<FlightSchedule> flightplans = new List<FlightSchedule>();

        public void StartSimulation()
        {
            flightplans.Add(new FlightSchedule("LON", 1, DateTime.Now));
            flightplans.Add(new FlightSchedule("STO", 2, DateTime.Now));
            flightplans.Add(new FlightSchedule("CPH", 3, DateTime.Now));

            //add gates
            Gates.Add(new Gate(1, 20, ""));
            Gates.Add(new Gate(2, 20, ""));
            Gates.Add(new Gate(3, 20, ""));

            desks.Add(new Desk("Desk1"));
            desks.Add(new Desk("Desk2"));

            SortingSystem sortingSystem = new SortingSystem();



            Thread controler = new Thread(ControllerMethod);
            controler.Name = "Manager";
            controler.Priority = ThreadPriority.AboveNormal;

            Thread sortT = new Thread(sortingSystem.SplitterMethod);
            sortT.Name = "SortingThread";
            sortT.Priority = ThreadPriority.Highest;


            controler.Start();




            //desks[0].StartDesk();
            //desks[1].StartDesk();

            sortT.Start();

            ////start all gate threads
            //foreach (var item in Gates)
            //{
            //    item.StartGate();
            //}

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
                Console.WriteLine("List of open desks press 2");
                Console.WriteLine("Close desk press 3");

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
                        ShowOpenDesks();
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
                    default:
                        break;
                }
                Console.ReadLine();

                Console.Clear();
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

        #region Controller Methods
        void ControllerMethod()
        {
            while (true)
            {
                foreach (var item in flightplans)
                {
                    // and if its not already begin
                    if (item.From <= DateTime.Now && !item.IsStarted)
                    {
                        CheckInControl(item);
                        item.IsStarted = true;
                    }
                }

                //if there is any there should take off
                if (flightplans.Any(fp => fp.To >= DateTime.Now))
                {
                    //temp to be sure is the same FlightPlan the mothod will touch
                    FlightSchedule plan = flightplans.Where(fp => fp.To >= DateTime.Now).First();
                    //TODO: Could let user decide if gate should forced to close
                    TimeToTakeOff(plan);
                    flightplans.Remove(plan);
                }
            }
        }

        void CheckInControl(FlightSchedule item)
        {
            //we found the gate there match the flightplans gatenumber
            Gate thisGate = Gates.Where(g => g.GateNumber == item.GateNum).FirstOrDefault();

            //If gate is alive nothing happend
            //while (thisGate.gateT == null ? false : true)
            //{
            //    Debug.WriteLine($"{thisGate.gateT} is Alive");
            //}

            //Update departures list
            UpdateDepartures(thisGate.Departure, item.Departure);

            //We set the departure on the gate
            thisGate.Departure = item.Departure;


            //start gate
            thisGate.StartGate();
            Logging.WriteToLog($"{thisGate.gateT.Name} is opened");


            //all gates have 10 seconds from this step.
            //add "to" date time which is from now and 10 sec
            item.To = DateTime.Now.AddSeconds(10);


            //if all desk is (null) not alive then wake them all
            if (desks.All(d => d.deskT == null))
            {
                foreach (var desk in desks)
                {
                    desk.StartDesk();
                    Logging.WriteToLog($"{desk.deskT.Name} is opened");
                }
            }
        }

        void TimeToTakeOff(FlightSchedule item)
        {
            //we found the gate there match the flightplans gatenumber
            Gate gateToClose = Gates.Where(g => g.GateNumber == item.GateNum).FirstOrDefault();


            if (gateToClose.gateT.IsAlive)
            {
                //If gate still working give them a second
                Thread.Sleep(1000);

                Logging.WriteToLog($"{gateToClose.gateT.Name} forced to close");

                //Update departures list
                UpdateDepartures(gateToClose.Departure, "");

                //We set the departure on the gate
                gateToClose.Departure = "";

                //close gate
                gateToClose.CloseGate();
                Logging.WriteToLog($"{gateToClose.gateT.Name} is closed");

            }
            else
            {
                //Update departures list
                UpdateDepartures(gateToClose.Departure, "");

                //We set the departure on the gate
                gateToClose.Departure = "";
            }

            //if all gates is (null) not alive then close all desks
            if (Gates.All(d => d.gateT == null))
            {
                foreach (var desk in desks)
                {
                    desk.CloseDesk();
                    Logging.WriteToLog($"{desk.deskT.Name} is closed");

                }
            }
        }

        void UpdateDepartures(string olddep, string newdep)
        {
            departures.Remove(olddep);

            departures.Add(newdep);
        }
        #endregion
    }
}