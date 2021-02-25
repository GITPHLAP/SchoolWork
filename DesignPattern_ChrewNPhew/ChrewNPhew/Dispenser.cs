using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ChrewNPhew
{
    public class Dispenser
    {
        //Constant int 
        const int maxGum = 55;

        private static Dispenser instance;

        List<Gum> gumList = new List<Gum>();

        public List<Gum> GumList { get => gumList; private set => gumList = value; }

        //create instance to dispenser
        public static Dispenser Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Dispenser();
                }
                return instance;
            }
        }

        //Constructor
        private Dispenser()
        {
            FillAll();
        }

        //Fill different gum types
        void Fill(GumColor gumColor)
        {
            //Round the amount of gum percent
            int c = (int)Math.Round(((maxGum / 100.0) * (int)gumColor));
            for (int i = 0; i < c; i++)
            {
                //if count not smaller then 55 then we dont add anymore
                if (GumList.Count < 55)
                {
                    //Add gum to list
                    GumList.Add(new Gum(gumColor));
                }
            }
        }

        public void FillAll()
        {
            if (gumList.Count != 0)
            {
                throw new NotImplementedException();
            }
            //foreach type in enum we fill 
            foreach (var item in Enum.GetValues(typeof(GumColor)))
            {
                Fill((GumColor)item);
            }

            //Here We "shake" the dispenser :)
            Random rnd = new Random();
            gumList = gumList.OrderBy(x => rnd.Next()).ToList();
        }

        public Gum GetGum()
        {
            Random random = new Random();

            //Get a random gum
            int index = random.Next(gumList.Count);

            //select which gum we should return 
            Gum gumtoreturn = gumList[index];
            
            // remove gum from list
            gumList.RemoveAt(index);

            return gumtoreturn;
        }

    }




}
