using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleBaggageHandlingSystem
{
    public class Desk
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
            if (deskT is Thread)
                StopThread = true;

        }

        public void ProduceBottles()
        {
            //To pick a random index 
            Random random = new Random();

            while (!StopThread)
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
                if (SortingSystem.AllLuggages.IsLimitReached())
                {
                    //Console.BackgroundColor = ConsoleColor.Red;
                    //wait for a push
                    Logging.WriteToLog($"{Thread.CurrentThread.Name} wait buffer is full");

                    Monitor.Enter(SortingSystem.AllLuggages);

                    Monitor.PulseAll(SortingSystem.AllLuggages);

                    Monitor.Wait(SortingSystem.AllLuggages);
                    Monitor.Exit(SortingSystem.AllLuggages);

                }
                else if (Monitor.TryEnter(SortingSystem.AllLuggages) && !SortingSystem.AllLuggages.IsLimitReached())
                {

                    //int counter = 1;

                    ////if limit not reache then do the stuff in whie

                    //bool testbool = SimulationManager.Gates.Any(g => g.Destination == null);


                    //get a random number from 0 to 2 
                    int tempindex = random.Next(SimulationManager.Gates.Where(g => g.Destination != null).Count());

                    //take a reservation
                    //var validreservation = ReservationSystem.Reservations.First();

                    //find valid a reservation with contains a flightplan equals to one of the gates
                    var validreservation = (ReservationSystem.Reservations.Join(SimulationManager.Gates,
                       r => r.schedule, g => g.GateSchedule,
                       (r, g) => new
                       {
                           r.PassengerNumber,
                           r.Name,
                           r.schedule
                       }
                       ).FirstOrDefault());

                    //Take the reservation which is equal to the valid reservation 
                    // We need this so we can remove it later in program.
                    Reservation reservation = ReservationSystem.Reservations.Where(r => r.schedule.Equals(validreservation.schedule)).FirstOrDefault();

                    ReservationSystem.Reservations.Remove(reservation);
                    //take name from index of varians-list and add it to a new bottle 
                    SortingSystem.AllLuggages.Enqueue(new Luggage(reservation));

                    //Console.BackgroundColor = ConsoleColor.Red;
                    Logging.WriteToLog($"{Thread.CurrentThread.Name} enqueue: {SimulationManager.Gates.Where(g => g.Destination != null).ToList()[tempindex].Destination}");

                    //increase counter
                    //counter++;

                    Monitor.PulseAll(SortingSystem.AllLuggages);
                    Monitor.Exit(SortingSystem.AllLuggages);

                    Thread.Sleep(500);

                }


            }
        }
    }
}
