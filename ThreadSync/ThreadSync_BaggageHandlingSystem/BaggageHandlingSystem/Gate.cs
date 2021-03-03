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
        //TODO: GateThread - make method instead off public 
        public Thread gateT;

        //GateNumber
        int gateNumber;
        
        string departure;
        
        //buffer to gates
        BufferQueue<Luggage> luggagesBuffer;

        public int GateNumber { get => gateNumber; private set => gateNumber = value; }
        public string Departure { get => departure; set => departure = value; }
        internal BufferQueue<Luggage> LuggagesBuffer { get => luggagesBuffer; set => luggagesBuffer = value; }




        //constructor to create a max size on gate and give it a number
        public Gate(int number, int luggageLimit, string departure)
        {
            gateNumber = number;
            luggagesBuffer = new BufferQueue<Luggage>(luggageLimit);
            this.departure = departure;
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
            if (gateT is Thread)
                gateT.Abort();

        }



        void Consume()
        {
            while (true)
            {

                Thread.Sleep(500);

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
                    else if (luggagesBuffer.IsLimitReached())
                    {
                        //send them all at once (Simulate flight)
                        for (int i = 0; i < luggagesBuffer.Count; i++)
                        {
                            //take one from queue
                            luggagesBuffer.Dequeue();

                            Logging.WriteToLog($"GateNumber {gateNumber} deque");
                        }

                        Monitor.Exit(luggagesBuffer);

                        //automaticly close gate when flight is take off
                        this.gateT.Abort();
                        Logging.WriteToLog($"{gateT} is closed by it self");
                    }
                    
                }

            }
        }
    }
}
