using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBankUT
{
    public class Account
    {
        public double Balance { get; set; }

        public Card Card { get; }

        /// <summary>
        /// Subtracts the amount from the balance
        /// </summary>
        /// <param name="amount"></param>
        /// <returns> Returns 0 if something goes wrong; else returns amount</returns>
        public double Withdraw(double amount)
        {
            if (CanWithdraw(amount))
            {
                Balance -= amount;
                return amount;
            }
            return 0;
        }
        public bool CanWithdraw(double amount)
        {
            if (amount <= 0)
            {
                return false;
            }
            return Balance >= amount;
        }

        public Account(double balance, Card card)
        {
            Balance = balance;
            this.Card = card;
        }
    }
}
