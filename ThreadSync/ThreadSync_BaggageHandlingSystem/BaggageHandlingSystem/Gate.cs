using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace BaggageHandlingSystem
{
    public class Gate
    {
        bool StopThread;

        //TODO: GateThread - make method instead off public 
        public Thread gateT;

        FlightSchedule gateSchedule = null;

        //GateNumber
        int gateNumber;

        //buffer to gates
        BufferQueue<Luggage> luggagesBuffer;

        public int GateNumber { get => gateNumber; private set => gateNumber = value; }
        public string Destination { get => gateSchedule == null ? null : gateSchedule.Destination;}
        internal BufferQueue<Luggage> LuggagesBuffer { get => luggagesBuffer; set => luggagesBuffer = value; }




        //constructor to create a max size on gate and give it a number
        public Gate(int number, int luggageLimit)
        {
            gateNumber = number;
            luggagesBuffer = new BufferQueue<Luggage>(luggageLimit);
        }

        //Method to start Desk thread
        public void StartGate()
        {
            //Start only if thread is null
            if (gateT == null)
            {
                gateT = new Thread(Consume);
                gateT.Name = $"Gate{gateNumber}";

                gateT.Start();
            }


        }

        //Method to close Desk Thread
        public void CloseGate()
        {
            Logging.WriteToLog($"{this.gateT.Name} is closed");
            if (gateT is Thread)
                this.StopThread = true;

        }



        void Consume()
        {
            while (!StopThread)
            {
                
                if (IsThereAnyDestinationsNow())
                {

                    //Return all 
                    SendLuggageToLostLuggage();

                    this.gateSchedule = NextFlightSchedule();

                    //send signal to desk that they can begin to create luggage
                    lock (SimulationManager.CentralLock)
                    {
                        Monitor.PulseAll(SimulationManager.CentralLock);
                    }

                    //this part should run while there is a flightschedule 
                    while (gateSchedule != null)
                    {
                        lock (luggagesBuffer)
                        {
                            //if buffer empty
                            if (luggagesBuffer.Count == 0)
                            {
                                //invoke threads who waiting 
                                Monitor.PulseAll(luggagesBuffer);

                                Monitor.Wait(luggagesBuffer);
                            }
                        }

                        lock (luggagesBuffer)
                        {
                            //try to enter buffer
                            if (luggagesBuffer.IsLimitReached() || TimeToTakeOf())
                            {

                                //send them all at once (Simulate flight)
                                for (int i = luggagesBuffer.Count; i > 0; i--)
                                {
                                    //take one from queue
                                    luggagesBuffer.Dequeue();

                                    Logging.WriteToLog($"GateNumber {gateNumber} deque");
                                }

                                //UpdateDestination(this.destination, null);



                                //set the flightplan to done 
                                Logging.WriteToLog($"FlightPlan to{gateSchedule.Destination} between {gateSchedule.Arrival} to {gateSchedule.Departure} flight now :{DateTime.Now} ");

                                FlightplanToDone(gateSchedule);

                                //remove flightschedule so sortingsystem not send package to this gate
                                this.gateSchedule = null;

                                //Tell sortingSystem that Flight has take off 
                                Monitor.PulseAll(luggagesBuffer);

                            }
                        }


                    }

                }
                else
                {
                    if (!IsThereAnyDestinations())
                    {
                        //Return the luggage
                        SendLuggageToLostLuggage();

                        Logging.WriteToLog($"Gate {gateNumber} wait for destinations");
                        
                        lock (SimulationManager.CentralLock)
                        {
                            Monitor.Wait(SimulationManager.CentralLock);
                        }
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }

                }

                

            }
        }

        //Return the luggage there missed the flight
        void SendLuggageToLostLuggage()
        {
            lock (SortingSystem.LostLuggages)
            {
                //Remove all lost luggage and add it lost package list
                while (luggagesBuffer.Count > 0)
                {
                    Logging.WriteToLog($"{Thread.CurrentThread.Name} add {Destination} to lostluggage");
                    SortingSystem.LostLuggages.Add(luggagesBuffer.Dequeue());
                }
            }
        }

        bool TimeToTakeOf()
        {
            return DateTime.Now >= SimulationManager.Flightplans.Where(f => f.GateNum == this.gateNumber && !f.IsDone).First().Departure;
        }

        void FlightplanToDone(FlightSchedule flightplan)
        {
            flightplan.IsDone = true;
        }

        FlightSchedule NextFlightSchedule()
        {
            return SimulationManager.Flightplans.Where(f => f.GateNum == this.gateNumber && !f.IsDone && DateTime.Now >= f.Arrival ).FirstOrDefault();
        }

        bool IsThereAnyDestinations()
        {
            return SimulationManager.Flightplans.Any(f => f.GateNum == this.gateNumber && !f.IsDone);
        }

        bool IsThereAnyDestinationsNow()
        {
            return SimulationManager.Flightplans.Any(f => f.GateNum == this.gateNumber && !f.IsDone && DateTime.Now >= f.Arrival);
        }

        //void UpdateDestination(string olddep, string newdep)
        //{


        //    SimulationManager.Destinations.Remove(olddep);
        //    if (newdep != null)
        //    {
        //        SimulationManager.Destinations.Add(newdep);
        //    }
        //}



    }
}
