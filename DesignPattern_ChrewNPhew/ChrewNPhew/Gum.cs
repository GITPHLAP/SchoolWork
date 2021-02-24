using System;
using System.Collections.Generic;
using System.Text;

namespace ChrewNPhew
{
    public enum GumColor { Blue, Purple, Yellow, Orange = 3, Red = 4, Green = 5 }
    public enum GumColorPercent { Blue = 25, Purple = 12, Yellow = 20, Orange = 19, Red = 14, Green = 10 }




    /// <summary>
    /// MAX 55 gums
    /// Blue 25%
    /// Purple 12%
    /// Yellow 20%
    /// Orange 19%
    /// Red 14%
    /// Green 10%
    /// Blue = "Blåbær", Purple = "Brombær", Yellow = "Tutti frutti", Orange = "Appelsin", Red = "Jordbær", Green = "Æble"
    /// </summary>

    //Blue 25%
    //
    class Gum
    {

        GumColor gumcolor;
        


        void TestRandom()
        {
            GumColor gctest = 0;


            int test1 = 0;


            //GumColor colortopercent = (GumColor)Enum.Parse(typeof(GumColor), test1);


            List<int> testlist = new List<int>() { 0, 1, 2, 3, 4, 5 };

            List<Gum> indispenserlist = new List<Gum>();
            Random rnd = new Random();
            while (indispenserlist.Count < 55)
            {

                int toadd = rnd.Next(6);


                if (indispenserlist.FindAll(x => (int)x.gumcolor == toadd).Count < 3)
                {
                    
                }

                switch (toadd)
                {
                    case GumColor.Blue==toadd:

                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    default:
                        break;
                }
            }



        }

        void CalculatePercent(Col inttoadd, int maxgums)
        {
           Math.Round(maxgums*(100/))
        }


    }


    
}
