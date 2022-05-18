using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBankUT
{
    public class Card
    {
        public string Code { get;}
        public int PinCode { get;}

        public Card(string code, int pincode)
        {
            Code = code;
            PinCode = pincode;
        }
    }
}
