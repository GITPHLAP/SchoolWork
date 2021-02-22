using System;
using System.Collections.Generic;
using System.Text;

namespace MyBanker
{
    public class Visa : Card, IExpirationCard
    {
        /// <summary>
        /// Jeg går udfra 20.000 er en månedelig overtræks grænse
        /// TILHØR VISA/DANKORT
        /// Dette er et delvist kreditkort med en grænse på 20.000 kr. Man skal være 18,
        /// førend dette kort kan udstedes.Dette kort kan gå i overtræk og man kan hæve
        /// op til 25.000 kr.på dette kort om måneden
        /// </summary>
        /// 
        double overdraftlimit = 20000;
        string[] prefix = { "4" };

        public DateTime Experiation { get => Experiation; set => DateTime.Now.AddYears(5); }


        public Visa(string cardHolderName, Account account)
        {
            Cardnum = GenerateCardNum(prefix);
            CardHolderName = cardHolderName;
            Account = account;
            InterUseable = false;
            MonthlyLimit = 25000;
        }


        protected override bool Validate(bool internationalterminal, double howmuchmoney) //internationalterminal just to know where
        {
            //if card only can use in Denmark and the terminal is placed in Denmark OR card also can be used internatinal
            if (InterUseable != internationalterminal && InterUseable != true && internationalterminal != false)
            {
                return false;
            }

            if ((Account.MonthlyUse + howmuchmoney) > MonthlyLimit)
            {
                return false;
            }

            //balance - transaction may not be more overdraft then the limit
            if ((Account.Balance - howmuchmoney) > overdraftlimit)
            {
                return false;
            }

            return true;
        }

    }
}
