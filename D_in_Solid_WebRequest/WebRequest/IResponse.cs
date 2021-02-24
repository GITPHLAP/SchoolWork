using System;
using System.Collections.Generic;
using System.Text;

namespace WebRequester
{
    public interface IResponse<T,K>
    {
        T Response(K request);
    }
}
