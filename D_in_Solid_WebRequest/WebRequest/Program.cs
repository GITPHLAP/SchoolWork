using System;
using System.IO;

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

            Console.WriteLine("--------------------------------------------------");
            LocalFileRequester fileRequester = new LocalFileRequester();

            //Set the full path on file
            Directory.SetCurrentDirectory(@"C:\");
            //put relative path 
            fileRequester.WriteToConsole(@"Users\kjedt\Documents\Test.txt");

        }
    }
}
