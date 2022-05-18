using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBankUT
{
    internal class SqlDatabase : IDAL
    {
        public List<Account> GetAccounts()
        {
            throw new NotImplementedException();
        }

        public void NewCard(string code, int pinkode)
        {
            throw new NotImplementedException();
        }

        public bool ValidateCard(string cardnum, int pincode)
        {
            throw new NotImplementedException();
        }
    }
}
