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

        //GateNumber
        int gateNumber;

        string destination;

        //buffer to gates
        BufferQueue<Luggage> luggagesBuffer;

        public int GateNumber { get => gateNumber; private set => gateNumber = value; }
        public string Destination { get => destination; set => destination = value; }
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



                    this.destination = NextDestination();

//to set destinations to a destination list
//SimulationManager.Destinations.Add(this.destination);

                    //send signal to desk that they can begin to create baggage
                    lock (SimulationManager.CentralLock)
                    {
                        Monitor.PulseAll(SimulationManager.CentralLock);
                    }




                    //try to enter buffer
                    if (Monitor.TryEnter(luggagesBuffer))
                    {
                        //if buffer empty
                        if (luggagesBuffer.Count == 0)
                        {
                            //invoke threads who waiting 
                            Monitor.PulseAll(luggagesBuffer);

                            Monitor.Wait(luggagesBuffer);
                        }
                        else if (luggagesBuffer.IsLimitReached() || TimeToTakeOf())
                        {
                            //send them all at once (Simulate flight)
                            for (int i = luggagesBuffer.Count; i > 0; i--)
                            {
                                //take one from queue
                                luggagesBuffer.Dequeue();

                                Logging.WriteToLog($"GateNumber {gateNumber} deque");
                            }

                            //remove destination so sortingsytem not send package to this gate
                            UpdateDepartures(this.destination, null);
                            this.destination = null;

                            //to be sure its the same plan 
                            FlightSchedule schedule = SimulationManager.Flightplans.Where(f => f.GateNum == this.gateNumber && !f.IsDone).First();
                            //set the flightplan to done 
                            Logging.WriteToLog($"FlightPlan to{schedule.Distination} between {schedule.Arrival} to {schedule.Departure} flight now :{DateTime.Now} ");

                            FlightplanToDone(schedule);
                        }

                        Monitor.Exit(luggagesBuffer);
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
                while (true)
                {
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

        string NextDestination()
        {
            return SimulationManager.Flightplans.Where(f => f.GateNum == this.gateNumber && !f.IsDone && DateTime.Now >= f.Arrival ).FirstOrDefault().Distination;
        }

        bool IsThereAnyDestinations()
        {
            return SimulationManager.Flightplans.Any(f => f.GateNum == this.gateNumber && !f.IsDone);
        }

        bool IsThereAnyDestinationsNow()
        {
            return SimulationManager.Flightplans.Any(f => f.GateNum == this.gateNumber && !f.IsDone && DateTime.Now >= f.Arrival);
        }

        void UpdateDepartures(string olddep, string newdep)
        {


            SimulationManager.Destinations.Remove(olddep);
            if (newdep != null)
            {
                SimulationManager.Destinations.Add(newdep);
            }
        }



    }
}
