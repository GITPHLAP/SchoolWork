using System;
using System.Collections.Generic;
using System.Text;

namespace Fight_ArenaPt2
{
    public class Knight : IFighter
    {


        //Declare knight attack range
        int attackrangemin = 1;
        int attackrangemax = 6;
        int defendpoint = 30;
        bool hasescaped = false;


        //set to defendpoint 
        public int DefenseLeft => defendpoint;


        public int Attack()
        {
            Random rndattack = new Random();

            //+1 because thats how random works to its between 1 and 6
            return rndattack.Next(attackrangemin, attackrangemax+1);
        }

        public void Defend(int attack)
        {
            //take the attack from defendpoint
            defendpoint -= attack;

            // after attack the knight can be very scared and run away
            Random number = new Random();

            //101 because then it from 1 to 100 
            int nrToCheck = number.Next(1, 101);
            //If number is between 1 and 15 which is 15 percent change for it then return true.
            if (nrToCheck <= 15)
            {
                hasescaped = true;
            }
            else
            {
                hasescaped = false;
            }
        }

        public bool HasEscaped()
        {
            return hasescaped;
        }
    }
}
