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
                ConsumeBottles();
            }
        }

        public void Split(Gate gate, Luggage luggage)
        {
            //This should never happend
            if (gate.LuggagesBuffer.IsLimitReached())
            {
                //wait for a push
                //Console.BackgroundColor = ConsoleColor.Yellow;
                Logging.WriteToLog($"Consumer/Splitter has overload {gate.gateT.Name}...");
                Monitor.Enter(gate.LuggagesBuffer);

                Monitor.Wait(gate.LuggagesBuffer);

                Monitor.Exit(gate.LuggagesBuffer);
            }


            //if limit not reache then do the
            if (Monitor.TryEnter(gate.LuggagesBuffer) && !gate.LuggagesBuffer.IsLimitReached())
            {

                //take name from index of varians-list and add it to a new bottle 
                gate.LuggagesBuffer.Enqueue(luggage);

                //Console.BackgroundColor = ConsoleColor.Yellow;
                Logging.WriteToLog($"Consumer/Splitter add luggage to {luggage.Departure} " );


                Monitor.Pulse(gate.LuggagesBuffer);

                Monitor.Exit(gate.LuggagesBuffer);

            }
        }

        public void ConsumeBottles()
        {
            Thread.Sleep(500);

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

            //take one from queue and add it to temp bottle 
            Luggage templuggage = AllLuggages.Dequeue();

            //Console.BackgroundColor = ConsoleColor.Yellow;
            Logging.WriteToLog($"{Thread.CurrentThread.Name} Consume luggage to: {templuggage.Departure}");
            
            
            Thread.Sleep(100);

            //TODO: Use FlightScheduler HERE!!
            foreach (var item in SimulationManager.Gates)
            {
                if (item.Departure == templuggage.Departure)
                {
                    Split(item, templuggage);
                }
                else //add "Lost luggage" to a list
                {
                    LostLuggages.Add(templuggage);
                }
            }

            Monitor.Exit(AllLuggages);


        }
    }
}
