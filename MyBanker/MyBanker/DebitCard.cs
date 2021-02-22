using System;
using System.Collections.Generic;
using System.Text;

namespace MyBanker
{
    public class DebitCard : Card
    {
        string[] prefix = { "4026", "417500", "4508", "4844", "4913", "4917" };

        public DebitCard(string cardHolderName, Account account)
        {
            Cardnum = GenerateCardNum(prefix);
            CardHolderName = cardHolderName;
            Account = account;
            InterUseable = false;
            MonthlyLimit = 0;
        }
    }
}
