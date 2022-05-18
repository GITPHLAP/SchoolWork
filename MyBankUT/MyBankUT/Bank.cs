using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBankUT
{
    public class Bank
    {
        public string Name { get; }
        private List<Account> Accounts = new List<Account>();
        private IDAL sqlDatabase;
        private List<IATM> aTMs;

        private void AddAtm()
        {
            throw new NotImplementedException();
        }

        public bool ValidateCard(string cardCode, int cardPincode)
        {
            return sqlDatabase.ValidateCard(cardCode, cardPincode);
        }

        public bool ProcessAction(ProcessTypes process, string cardnumber, double amount)
        {
            // Check that amount is a negative number or zero
            if (amount <= 0)
            {
                return false;
            }
            switch (process)
            {
                case ProcessTypes.WithDraw:

                    //if (Accounts.Any(a => a.Card.Code == cardnumber)) //Check if account exist with that cardnumber
                    //{
                    //    if (Accounts.First(a => a.Card.Code == cardnumber).CanWithdraw(amount)) //Check if it can withdraw else return false
                    //    {
                    //        Accounts.First(a => a.Card.Code == cardnumber).Withdraw(amount);
                    //        return true;
                    //    }
                    //}
                    //    return false;
                    return Accounts.FirstOrDefault(a => a.Card.Code == cardnumber)?.Withdraw(amount) > 0;
                default:
                    return false;
            }
        }

        public Bank(string name, IDAL dal, List<IATM> atms)
        {
            Name = name;
            this.sqlDatabase = dal;
            this.aTMs = atms;

            Accounts = sqlDatabase.GetAccounts();
        }
    }

    public enum ProcessTypes
    {
        WithDraw
    }
}
