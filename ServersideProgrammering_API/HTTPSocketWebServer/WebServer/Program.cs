using System;

namespace WebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //System.Net.IPAddress address = System.Net.IPAddress.Parse("10.108.130.121");
            System.Net.IPAddress address = System.Net.IPAddress.Parse("192.168.0.181");
            HttpServer server = new HttpServer();

            server.running = true;

            server.Start(address, 11000);


        }
    }
}
