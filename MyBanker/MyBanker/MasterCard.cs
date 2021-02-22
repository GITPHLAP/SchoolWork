using System;
using System.Collections.Generic;
using System.Text;

namespace MyBanker
{
    public class MasterCard : Card, IExpirationCard
    {
        //prefix to cardnumber
        string[] prefix = { "4" };


        /// <summary>
        /// Jeg kunne måske godt have lavet en KreditKort klasse, men blev meget fovirret over Visa/Dankort 
        /// og har derfor ikke lavet den
        /// </summary>
        private double dailyLimit = 5000;
        private double monthlyCreditLimit = 40000;

        public DateTime Experiation { get => Experiation; set => DateTime.Now.AddYears(5); }


        public MasterCard(string cardHolderName, Account account)
        {
            Cardnum = GenerateCardNum(prefix);
            CardHolderName = cardHolderName;
            Account = account;
            InterUseable = false; //Can't be used in other contrys then Denmark
            MonthlyLimit = 30000;
        }


        protected override bool Validate(bool internationalterminal, double howmuchmoney) //internationalterminal just to know where
        {
            //if card only can use in Denmark and the terminal is placed in Denmark OR card also can be used internatinal
            if (InterUseable != internationalterminal && InterUseable != true && internationalterminal != false)
            {
                return false;
            }

            if ((Account.DailyUse + howmuchmoney) > dailyLimit)
            {
                return false;
            }

            if ((Account.MonthlyUse + howmuchmoney) > MonthlyLimit)
            {
                return false;
            }

            if (howmuchmoney > monthlyCreditLimit)
            {
                return false;
            }

            return true;
        }
    }
}
