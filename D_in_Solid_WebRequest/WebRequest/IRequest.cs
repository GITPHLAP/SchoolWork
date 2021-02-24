using System;
using System.Collections.Generic;
using System.Text;

namespace WebRequester
{
    public interface IRequest<T>
    {
        T Requester(string path);
    }
}
