using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleBaggageHandlingSystem
{
    public class SortingSystem
    {
        //buffer to sorting system
        public static List<Luggage> LostLuggages = new List<Luggage>();

        public static BufferQueue<Luggage> AllLuggages { get; private set; }

        public event EventHandler UpdateGatesLuggage;



        public SortingSystem()
        {
            AllLuggages = new BufferQueue<Luggage>(20);
        }

        public void SplitterMethod()
        {
            while (true)
            {
                //this wait for destinations
                //stay in while if there is not a destination
                if (SimulationManager.Gates.All(g => g.Destination == null))
                {
                    Logging.WriteToLog($"{Thread.CurrentThread.Name} wait for destinations");

                    lock (SimulationManager.CentralLock)
                    {
                        Monitor.Wait(SimulationManager.CentralLock);
                    }
                }
                else
                {

                    ConsumeBottles();
                }
            }
        }

        public void Split(Gate gate, Luggage luggage)
        {
            //This should never happend
            if (gate.LuggagesBuffer.IsLimitReached())
            {
                Logging.WriteToLog($"Consumer/Splitter has fill {gate.gateT.Name}...");
                Monitor.Enter(gate.LuggagesBuffer);

                Monitor.Pulse(gate.LuggagesBuffer);

                Monitor.Wait(gate.LuggagesBuffer);

                Monitor.Exit(gate.LuggagesBuffer);
            }


            //if limit not reache then do the
            if (Monitor.TryEnter(gate.LuggagesBuffer) && !gate.LuggagesBuffer.IsLimitReached())
            {

                //take name from index of varians-list and add it to a new bottle 
                gate.LuggagesBuffer.Enqueue(luggage);

                //Send an event to all listeners that GatesLuggage should update
                UpdateGatesLuggage?.Invoke(this, EventArgs.Empty);

                //Console.BackgroundColor = ConsoleColor.Yellow;
                Logging.WriteToLog($"Consumer/Splitter add luggage to {luggage.Destination}");

                // if gate luggagebuffer fill send a signal to gate
                if (gate.LuggagesBuffer.IsLimitReached() || AllLuggages.Count == 0 || gate.TimeToTakeOf())
                {
                    Monitor.PulseAll(gate.LuggagesBuffer);
                }

                Monitor.Exit(gate.LuggagesBuffer);

            }
        }


        public void ConsumeBottles()
        {

            //try to enter buffer
            Monitor.Enter(AllLuggages);

            //if buffer empty and wait for a pulse
            while (AllLuggages.Count == 0)
            {
                //invoke threads who waiting 
                Monitor.PulseAll(AllLuggages);

                Monitor.Wait(AllLuggages);
            }

            //TODO: I could use Flightschedule instead of gates.
            if (AnyLostLuggageMatchAnyGates())
            {
                SplitAllLostLuggage();
            }
            else if (SimulationManager.Gates.Any(g => g.GateSchedule == AllLuggages.Peek().Reservation.Schedule))
            {
                //Write to log file
                Logging.WriteToLog($"{Thread.CurrentThread.Name} Consume luggage to: {AllLuggages.Peek().Destination}");

                Gate test = SimulationManager.Gates.First(g => g.GateSchedule == AllLuggages.Peek().Reservation.Schedule);

                var test2 = 2;
                //method to Send luggage to gate 
                Split(SimulationManager.Gates.First(g => g.GateSchedule == AllLuggages.Peek().Reservation.Schedule), AllLuggages.Dequeue());
            }
            else //add "Lost luggage" to a list
            {
                //Write to log file
                Logging.WriteToLog($"{Thread.CurrentThread.Name} Consume luggage to: {AllLuggages.Peek().Destination} and add it to lostluggage");

                LostLuggages.Add(AllLuggages.Dequeue());
            }

            Monitor.Exit(AllLuggages);


        }

        bool AnyLostLuggageMatchAnyGates()
        {
            //find luggage in lost luggage with contains a destination equals to one of the gates
            var validLuggage = (LostLuggages.Join(SimulationManager.Gates,
               l => l.Destination, g => g.Destination,
               (l, g) => new
               {
                   l.LuggageNum
               }
               ).FirstOrDefault());

            return validLuggage == null ? false : true;
        }

        void SplitAllLostLuggage()
        {
            //find luggage in lost luggage with contains a destination equals to one of the gates
            var validLuggageList = (LostLuggages.Join(SimulationManager.Gates,
               l => l.Destination, g => g.Destination,
               (l, g) => new
               {
                   l.LuggageNum
               }
               ));
            foreach (var item in validLuggageList)
            {
                //Take the luggage which is equal to the valid luggagenumber 
                // We need this so we can remove it later in program.
                Luggage lostluggage = LostLuggages.Where(l => l.LuggageNum == item.LuggageNum).FirstOrDefault();

                //Write to log file
                Logging.WriteToLog($"{Thread.CurrentThread.Name} Consume luggage to: {lostluggage.Destination}");

                //method to Send luggage to gate 
                Split(SimulationManager.Gates.First(g => g.Destination ==lostluggage.Destination), lostluggage);
                
            }
            //remove all luggage from lostluggage
            LostLuggages.RemoveRange(0, LostLuggages.Count);
        }

    }
}
