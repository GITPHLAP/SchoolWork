using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VittighedsMaskinen.Authentication
{
    public interface IGetAllApiKeysQuery
    {
        Task<ApiKey> Execute(string providedApiKey);
    }
}
