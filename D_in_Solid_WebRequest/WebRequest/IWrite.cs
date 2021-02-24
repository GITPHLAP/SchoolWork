using System;
using System.Collections.Generic;
using System.Text;

namespace WebRequester
{
    interface IWrite<T>
    {
        void WriteToConsole(T response);
    }
}
