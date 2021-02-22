using System;
using System.Collections.Generic;
using System.Text;

namespace MyBanker
{
    public class Maestro : Card, IExpirationCard
    {
        string[] prefix = { "4026", "417500", "4508", "4844", "4913", "4917" };
        public DateTime Experiation { get => Experiation; set => DateTime.Now.AddYears(5).AddMonths(8); }

 
        public Maestro(string cardHolderName, Account account)
        {
            Cardnum = GenerateCardNum(prefix, 19);
            CardHolderName = cardHolderName;
            Account = account;
            InterUseable = true;
            MonthlyLimit = 0;
        }

    }
}
