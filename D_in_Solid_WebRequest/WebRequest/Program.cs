using System;

namespace WebRequester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ShowWebRequest showweb = new ShowWebRequest();

            showweb.WriteToConsole(
                showweb.Response(
                    showweb.Requester("https://docs.microsoft.com")
                    )
                );


        }
    }
}
