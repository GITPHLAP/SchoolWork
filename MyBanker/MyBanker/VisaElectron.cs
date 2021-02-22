using System;
using System.Collections.Generic;
using System.Text;

namespace MyBanker
{
    public class VisaElectron : Card, IExpirationCard
    {
        string[] prefix = { "4026", "417500", "4508", "4844", "4913", "4917" };

        public DateTime Experiation { get => Experiation; set => DateTime.Now.AddYears(5); }


        public VisaElectron(string cardHolderName, Account account)
        {
            Cardnum = GenerateCardNum(prefix);
            CardHolderName = cardHolderName;
            Account = account;
            InterUseable = true;
            MonthlyLimit = 10000;
        }
    }
}
