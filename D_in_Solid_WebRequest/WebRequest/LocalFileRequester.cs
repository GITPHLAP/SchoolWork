using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace WebRequester
{
    class LocalFileRequester : IRequest<StreamReader>, IWrite<string>
    {
        public StreamReader Requester(string path)
        {
            //if (!File.Exists(path))
            //{
            //    throw new FileNotFoundException("I couldn't find your file!");
            //}

            return File.OpenText(path);
        }

        public void WriteToConsole(string response)
        {

            //if (!File.Exists(response))
            //{
            //    throw new FileNotFoundException("I couldn't find your file!");
            //}

            Console.WriteLine(File.ReadAllText(response));
        }
    }
}
