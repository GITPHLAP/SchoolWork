using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VittighedsMaskinen.Token
{
    public interface IJwtAuth
    {
        string Authentication(Credentials user);
    }
}
