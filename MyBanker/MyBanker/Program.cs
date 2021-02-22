using System;

namespace MyBanker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //create 3 new accounts
            Account a1 = new Account(CreateTestAccountsNum(), "a1");

            Account a2 = new Account(CreateTestAccountsNum(), "a2");

            Account a3 = new Account(CreateTestAccountsNum(), "a3");

            //Create 3 cards 1 to each account
            Card mastercard = new MasterCard("Jens", a1);

            Card visa = new Visa("Erik", a2);

            Card maestro = new Maestro("Bo", a3);


            // Try to use all cards
            mastercard.Pay(false, 200.2);
            visa.Pay(false, 200.2);
            maestro.Pay(false, 200.2);







        }

        //Use to get random accountnumber
        static long CreateTestAccountsNum()
        {
            string reginumber = "3520";
            string accountnum = reginumber;

            Random acnum_random = new Random();

            while (accountnum.Length < 14)
            {
                accountnum += acnum_random.Next(0, 9);
            }

            return Convert.ToInt64(accountnum);
        }
    }
}
