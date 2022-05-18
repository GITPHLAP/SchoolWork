using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBankUT
{
    public interface IDAL
    {
        public void NewCard(string code, int pinkode);
        public bool ValidateCard(string cardnum, int pincode);
        public List<Account> GetAccounts();
    }
}
