using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace BaggageHandlingSystem
{
    class SortingSystem
    {
        //buffer to sorting system
        public static BufferQueue<Luggage> AllLuggages = new BufferQueue<Luggage>(20);

        public static List<Luggage> LostLuggages = new List<Luggage>();

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

                //Console.BackgroundColor = ConsoleColor.Yellow;
                Logging.WriteToLog($"Consumer/Splitter add luggage to {luggage.Departure}");

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
                //TODO: Pulse or PulseAll
                Monitor.PulseAll(AllLuggages);

                Monitor.Wait(AllLuggages);
            }


                if (SimulationManager.Gates.Any(g => g.Destination == AllLuggages.Peek().Departure))
                {
                    //Write to log file
                    Logging.WriteToLog($"{Thread.CurrentThread.Name} Consume luggage to: {AllLuggages.Peek().Departure}");

                    //method to Send luggage to gate 
                    Split(SimulationManager.Gates.First(g => g.Destination == AllLuggages.Peek().Departure), AllLuggages.Dequeue());
                }
                else //add "Lost luggage" to a list
                {
                    //Write to log file
                    Logging.WriteToLog($"{Thread.CurrentThread.Name} Consume luggage to: {AllLuggages.Peek().Departure} and add it to lostluggage");

                    LostLuggages.Add(AllLuggages.Dequeue());
                }
            







            //TODO: Use FlightScheduler HERE!!
            //foreach (var item in SimulationManager.Gates)
            //{
            //    if (item.Destination == AllLuggages.Peek().Departure)
            //    {
            //        //Write to log file
            //        Logging.WriteToLog($"{Thread.CurrentThread.Name} Consume luggage to: {AllLuggages.Peek().Departure}");
                    
            //        //method to Send luggage to gate 
            //        Split(item, AllLuggages.Dequeue());
            //    }
            //    else //add "Lost luggage" to a list
            //    {
            //        //Write to log file
            //        Logging.WriteToLog($"{Thread.CurrentThread.Name} Consume luggage to: {AllLuggages.Peek().Departure} and add it to lostluggage");

            //        LostLuggages.Add(AllLuggages.Dequeue());
            //    }
            //}

            Monitor.Exit(AllLuggages);


        }

    }
}
