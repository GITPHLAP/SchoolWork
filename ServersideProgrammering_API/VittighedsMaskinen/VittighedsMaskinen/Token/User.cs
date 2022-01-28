using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VittighedsMaskinen.Token
{
    public class User: Credentials
    {
        public List<string> Roles { get; set; }
    }
}
