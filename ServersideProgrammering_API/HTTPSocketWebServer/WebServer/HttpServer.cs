using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Unicode;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace WebServer
{
    public class HttpServer
    {
        private int timeout = 8;

        private Socket serverSocket;

        public bool running = false;

        private string contentPath = Directory.GetCurrentDirectory();


        public void Start(IPAddress ipAddress, int port, string contentPath = "")
        {

            try
            {
                //Create endpoint 
                IPEndPoint localEndpoint = new IPEndPoint(ipAddress, port);

                // tcp/ip socket
                serverSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);


                //bind server to local endpoint 
                serverSocket.Bind(localEndpoint);
                serverSocket.Listen();
                serverSocket.ReceiveTimeout = timeout;
                serverSocket.SendTimeout = timeout;

                while (running)
                {
                    Socket clientSocket;

                    try
                    {
                        //wait until client connect to server
                        clientSocket = serverSocket.Accept();

                        //Create a new thread each time we should handle a new client
                        Thread clientHandler = new Thread(() => 
                        {
                            clientSocket.ReceiveTimeout = timeout;
                            clientSocket.SendTimeout = timeout;

                            try
                            {
                                //handle client request
                                handleRequest(clientSocket);
                            }
                            catch
                            {
                                Console.WriteLine("Error when handleRequest from client");
                                clientSocket.Close();
                            }

                        });
                        clientHandler.Start();
                        
                    }
                    catch
                    {
                        Console.WriteLine("Error when connecting to client");
                        running = false; //stop server socket
                    }
                }

            }
            catch
            {

                throw;
            }
        }

        private void handleRequest(Socket clientSocket)
        {
            //Test multiThreading
            //Thread.Sleep(5000);

            //data buffer
            byte[] dataBuffer = new byte[1024];
            int receivedByteCount = clientSocket.Receive(dataBuffer);
            string strReceived = Encoding.UTF8.GetString(dataBuffer, 0, receivedByteCount);
            string[] receivedArr = strReceived.Split(" ");

            //get the HTTP method
            string httpMethod = receivedArr[0]; // the first part is always the method

            //get the requested parameter
            string requestedPar = receivedArr[1]; // the second is always the parameter
            string requestedFile = requestedPar;
            if (httpMethod == "GET" || httpMethod == "POST")
            {
                requestedFile = requestedFile.Replace("/", @"\").Replace("\\..", "");
            }
            else
            {
                //Implement status error code
                MethodNotAllowed(clientSocket);
                return;
            }

            if (requestedFile == @"\")
            {

                //Send default index.html
                if (System.IO.File.Exists(contentPath + requestedFile + "idex.html"))
                {
                    SendOKResponse(clientSocket, System.IO.File.ReadAllBytes(contentPath + requestedFile + "\\index.html"), "text/html");
                }
                else
                {
                    NotFound(clientSocket);
                }
            }
            else
            {
                //Send requested file
                if (System.IO.File.Exists(contentPath + requestedFile))
                {
                    SendOKResponse(clientSocket, System.IO.File.ReadAllBytes(contentPath + requestedFile), "text/html");
                }
                else
                {
                    NotFound(clientSocket);
                }
            }


            Console.WriteLine(strReceived);

        }

        private void MethodNotAllowed(Socket clientSocket)
        {
            SendResponse(clientSocket, "<html><head><meta http - equiv =\"Content-Type\" content=\"text/html; charset = utf - 8\">" +
                "</head><body><h2>Philips Webserver " +
                "</h2><div>405 - Method Not Allowed</div></body ></html> ",
                "405 Method Not Allowed", "text/html");
        }

        private void NotFound(Socket clientSocket)
        {
            SendResponse(clientSocket, "<html><head><meta http - equiv =\"Content-Type\" content=\"text/html; charset = utf - 8\">" +
                "</head><body><h2>Philips Webserver " +
                "</h2><div>404 - Not found</div></body ></html> ",
                "404 Not found", "text/html");
        }

        private void SendOKResponse(Socket clientSocket, byte[] bContent, string contentType)
        {
            SendResponse(clientSocket, bContent, "200 OK", contentType);
        }

        /// <summary>
        /// Convert response from string to bytes and then use the other method to send response
        /// </summary>
        /// <param name="clientSocket"></param>
        /// <param name="strContent"></param>
        /// <param name="responsCode"></param>
        /// <param name="contentType"></param>
        private void SendResponse(Socket clientSocket, string strContent, string responsCode, string contentType)
        {
            byte[] bContent = Encoding.UTF8.GetBytes(strContent);
            SendResponse(clientSocket, bContent, responsCode, contentType);
        }

        /// <summary>
        /// Send the response to client with content as bytes 
        /// </summary>
        /// <param name="clientSocket"></param>
        /// <param name="bContent"></param>
        /// <param name="responsCode"></param>
        /// <param name="contentType"></param>
        private void SendResponse(Socket clientSocket, byte[] bContent, string responsCode, string contentType)
        {
            try
            {
                byte[] header = Encoding.UTF8.GetBytes(
                    "HTTP/1.1" + responsCode + "\r\n"
                    + "Server: Philips server\r\n"
                    + "Content-Length" + bContent.Length.ToString() + "\r\n"
                    + "Connection: closed\r\n"
                    + "Content-Type: " + contentType + "\r\n\r\n");
                clientSocket.Send(header);
                clientSocket.Send(bContent);
                clientSocket.Close();
            }
            catch { Console.WriteLine("ERROR"); }
        }






        //default constructor 
        public HttpServer()
        {

        }
    }
}
