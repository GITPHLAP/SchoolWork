using System;
using System.Collections.Generic;
using System.Text;

namespace MyBanker
{
    public abstract class Card
    {
        //card number
        protected long Cardnum;

        protected string CardHolderName;

        //To get accountnumber
        protected Account Account;

        //Can card been used in other countrys then Denmark
        protected bool InterUseable;
        
        protected double MonthlyLimit;

        //Virtual so i can override if need in classes
        protected virtual bool Validate(bool internationalterminal, double howmuchmoney) //internationalterminal Is terminal locate in other countrys the Denmark
        {
            //IF card can be used in other countrys skip this 
            if (InterUseable != internationalterminal && InterUseable != true && internationalterminal != false)
            {
                return false;
            }

            //If the monthly use + transaction is bigger the monthly limit return false if limit equal zero skip
            if ((Account.MonthlyUse + howmuchmoney) > MonthlyLimit && MonthlyLimit != 0)
            {
                return false;
            }

            //if ballance - transaction is negativ then break
            if ((Account.Balance - howmuchmoney) < 0)
            {
                return false;
            }

            return true;
        }

        public void Pay(bool internationalterminal, double howmuchmoney) 
        {
            if (Validate(internationalterminal, howmuchmoney) == true)
            {
                Console.WriteLine("Payed");
            }
            else
            {
                Console.WriteLine("Failed");

            }
        }

        //Thoes two to genearete random cardnumber
        protected long GenerateCardNum(string[] prefixarray)
        {
            Random randomprefix = new Random();

            string cardnum = prefixarray[randomprefix.Next(prefixarray.Length)]; //pick random one of the prefix

            while (cardnum.Length < 16)
            {
                Random randomnum = new Random();
                cardnum += randomnum.Next(0, 9);
            }

            return Convert.ToInt64(cardnum);
        }

        protected long GenerateCardNum(string[] prefixarray, int cardnumLength)
        {
            Random randomprefix = new Random();

            string cardnum = prefixarray[randomprefix.Next(prefixarray.Length)]; //pick random one of the prefix

            while (cardnum.Length < cardnumLength)
            {
                Random randomnum = new Random();
                cardnum += randomnum.Next(0, 9);
            }

            return Convert.ToInt64(cardnum);
        }

    }
}
