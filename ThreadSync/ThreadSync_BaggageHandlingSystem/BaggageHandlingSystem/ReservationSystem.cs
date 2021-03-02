using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BaggageHandlingSystem
{
    public class ReservationSystem
    {
        static List<Reservation> Reservations = new List<Reservation>();


        //TODO: split this method... RememberSOLID
        public void ReadCSVFileAndAddToList(string filepath)
        {

            // Open file with Filestream
            FileStream streamfile = new FileStream(filepath, FileMode.Open, FileAccess.Read);

            StreamReader streamreader = new StreamReader(streamfile);

            bool IsFirstLine = true;

            while (!streamreader.EndOfStream)
            {
                string readerline = streamreader.ReadLine();

                if (!IsFirstLine)
                {
                    //split the line into arrays
                    string[] values = readerline.Split(';');

                    //add new reservatio to list and create a new reservation
                    Reservations.Add(new Reservation(Convert.ToInt32(values[0]), values[1], Convert.ToInt32(values[2])));
                }
                else
                {
                    IsFirstLine = false;
                }


            }
                
            //Close streamreader and file stream
            streamreader.Close();

            streamfile.Close();

        }


    }
}
