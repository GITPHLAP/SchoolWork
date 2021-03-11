using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleBaggageHandlingSystem
{
    public class Gate
    {
        public enum FlightStatus { Working, TakeOff, Landing, NoMoreFlight };
        bool StopThread;

        //TODO: GateThread - make method instead off public 
        public Thread gateT;

        private FlightSchedule gateSchedule = null;

        //GateNumber
        int gateNumber;

        //buffer to gates
        BufferQueue<Luggage> luggagesBuffer;

        // Var to the gates flight status
        FlightStatus status = FlightStatus.NoMoreFlight;

        public bool NoMoreSchedules { get => status == FlightStatus.NoMoreFlight ? true : false; }
        public int GateNumber { get => gateNumber; private set => gateNumber = value; }
        public string Destination { get => gateSchedule == null ? null : gateSchedule.Destination;}
        public BufferQueue<Luggage> LuggagesBuffer { get => luggagesBuffer; set => luggagesBuffer = value; }
        public FlightStatus Status { get => status; }
        public FlightSchedule GateSchedule { get => gateSchedule; }

        //handler which someone can listen on
        public event EventHandler GatesFlightStatus;


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
                    //Set status on gate that we got a flight in the gate
                    InvokeFlightStatus(FlightStatus.Landing);

                    //Return all 
                    SendLuggageToLostLuggage();

                    this.gateSchedule = NextFlightSchedule();

                    //send signal to desk that they can begin to create luggage
                    lock (SimulationManager.CentralLock)
                    {
                        Monitor.PulseAll(SimulationManager.CentralLock);
                    }

                    //this part should run while there is a flightschedule 
                    while (GateSchedule != null)
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
                                //They begin putting luggage to the flight 
                                InvokeFlightStatus(FlightStatus.Working);

                                //send them all at once (Simulate flight)
                                for (int i = luggagesBuffer.Count; i > 0; i--)
                                {
                                    //take one from queue
                                    luggagesBuffer.Dequeue();

                                    Logging.WriteToLog($"GateNumber {gateNumber} deque");
                                }


                                //set the flightplan to done 
                                Logging.WriteToLog($"FlightPlan to{GateSchedule.Destination} between {GateSchedule.Arrival} to {GateSchedule.Departure} flight now :{DateTime.Now} ");

                                //Its time to take off 
                                InvokeFlightStatus(FlightStatus.TakeOff);

                                FlightplanToDone(GateSchedule);

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
                        InvokeFlightStatus(FlightStatus.NoMoreFlight);

                        //Return the luggage
                        SendLuggageToLostLuggage();

                        Logging.WriteToLog($"Gate {gateNumber} wait for destinations");
                        
                        lock (SimulationManager.CentralLock)
                        {
                            //Write to console that gates do not have more Flight Schedules
                            SimulationManager.NoMoreFlightSchedules = true;
                            
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

        //Method to send event to listingers when updating FlightStatus
        void InvokeFlightStatus(FlightStatus newstatus)
        {
            status = newstatus;

            //invoker, say it to all listeners.
            GatesFlightStatus?.Invoke(this, EventArgs.Empty);
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



    }
}
