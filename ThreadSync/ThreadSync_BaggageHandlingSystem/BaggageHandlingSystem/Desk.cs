using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BaggageHandlingSystem
{
    class Desk
    {
        //TODO: DeskThread - make method instead off public 
        public Thread deskT;

        public bool StopThread;

        string deskName;

        public string DeskName { get => deskName; set => deskName = value; }

        public Desk(string deskName)
        {
            this.deskName = deskName;
        }




        //Method to start Desk thread
        public void StartDesk()
        {
            //Start only if thread is null
            if (deskT is null)
            {
                //StopThread = false;
                deskT = new Thread(ProduceBottles);
                deskT.Name = deskName;

                deskT.Start();
            }
                

        }

        //Method to close Desk Thread
        public void CloseDesk()
        {
            if(deskT is Thread)
                StopThread = true;

        }






        public void ProduceBottles()
        {
            //To pick a random index 
            Random random = new Random();

            while (!StopThread)
            {

                if (StopThread)
                {
                    Thread.CurrentThread.Abort();
                }
                else if (SortingSystem.AllLuggages.IsLimitReached())
                {
                    //Console.BackgroundColor = ConsoleColor.Red;
                    //wait for a push
                    Logging.WriteToLog($"{Thread.CurrentThread.Name} Closed");


                    Monitor.Enter(SortingSystem.AllLuggages);
                    Monitor.Wait(SortingSystem.AllLuggages);
                    Monitor.Exit(SortingSystem.AllLuggages);
                    
                }
                else if (Monitor.TryEnter(SortingSystem.AllLuggages) && !SortingSystem.AllLuggages.IsLimitReached())
                {

                    int counter = 1;

                    //if limit not reache then do the stuff in whie

                        //get a random number from 0 to 2 
                        int tempindex = random.Next(SimulationManager.departures.Count);

                        //take name from index of varians-list and add it to a new bottle 
                        SortingSystem.AllLuggages.Enqueue(new Luggage(SimulationManager.departures[tempindex].ToString()));

                        //Console.BackgroundColor = ConsoleColor.Red;
                        Logging.WriteToLog($"{Thread.CurrentThread.Name} enqueue: {SimulationManager.departures[tempindex]}");
                        //increase counter
                        counter++;
                        Monitor.PulseAll(SortingSystem.AllLuggages);
                    Monitor.Exit(SortingSystem.AllLuggages);

                    Thread.Sleep(500);



                }


            }
        }
    }
}
