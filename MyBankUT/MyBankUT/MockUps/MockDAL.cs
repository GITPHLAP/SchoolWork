using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBankUT.MockUps
{
    public class MockDAL : IDAL
    {
        public List<Account> GetAccounts()
        {
            List<Account> accounts = new List<Account>();
            accounts.Add(new Account(500,
                new Card("4015-5047-4531-5433", 1234)));

            accounts.Add(new Account(5050,
    new Card("5015-5047-4531-5433", 4321)));

            accounts.Add(new Account(1000,
    new Card("7015-5047-4531-0000", 1570)));

            accounts.Add(new Account(10000,
    new Card("9999-5047-4531-5433", 8080)));

            accounts.Add(new Account(20.203,
    new Card("0001-5047-4531-5433", 0001)));

            accounts.Add(new Account(0,
    new Card("1010-5047-4531-5433", 1010)));

            return accounts;

        }

        public void NewCard(string code, int pinkode)
        {
            throw new NotImplementedException();
        }

        public bool ValidateCard(string cardnum, int pincode)
        {
            var accounts = GetAccounts();
            return accounts.Any(a => a.Card.Code == cardnum && a.Card.PinCode == pincode);
        }
    }
}
