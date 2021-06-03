using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace KodeRefactoring
{
    public class GUI
    {
        static void Main()
        {
            Logic logic = new Logic();

            while (true)
            {


                //input to select what to do
                Console.WriteLine("Select one of followed commands");
                Console.WriteLine("GetIP: 0");
                Console.WriteLine("LocalPing: 1");
                Console.WriteLine("GetHN_IP: 2");
                Console.WriteLine("Tracert: 3");
                Console.WriteLine("LocalDHCPIP: 4");
                Console.WriteLine("ThisA_IP: 5");

                int input = 0;
                bool inputex;
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                    inputex = false;
                }
                catch (Exception)
                {
                    inputex = true;
                }

                if (!inputex)
                {
                    foreach (var item in MakeOutputToStrArray(input))
                    {
                        Console.WriteLine(item);

                    }
                        Console.ReadKey();//Wait for user to continue

                    //switch (input)
                    //{
                    //    case 0:
                    //        #region Get IP from Hostname
                    //        string hostname = "en.wikipedia.org";//Console.ReadLine();
                    //        Console.WriteLine($"{hostname}s IP adresses");
                    //        foreach (IPAddress ip in logic.GetIPArrayFromHostName(hostname))
                    //        {
                    //            Console.WriteLine(ip.ToString());
                    //        }
                    //        #endregion
                    //        Console.ReadKey();
                    //        break;
                    //    case 1:
                    //        #region LocalPing
                    //        //get local ping reply
                    //        PingReply localpingReply = logic.LocalPing();
                    //        //error handling - IF reply is success then write it else write status.
                    //        if (localpingReply.Status == IPStatus.Success)
                    //        {
                    //            Console.WriteLine("Address: {0}", localpingReply.Address.ToString());
                    //            Console.WriteLine("RoundTrip time: {0}", localpingReply.RoundtripTime);
                    //            Console.WriteLine("Time to live: {0}", localpingReply.Options.Ttl);
                    //            Console.WriteLine("Don't fragment: {0}", localpingReply.Options.DontFragment);
                    //            Console.WriteLine("Buffer size: {0}", localpingReply.Buffer.Length);
                    //        }
                    //        else
                    //        {
                    //            Console.WriteLine(localpingReply.Status);
                    //        }
                    //        #endregion
                    //        Console.ReadKey();
                    //        break;
                    //    case 2:
                    //        #region Get Hostname From IP and ip from Hostname
                    //        Console.WriteLine("start");
                    //        string t = logic.GetHostnameFromIp("8.8.8.8");
                    //        Console.WriteLine(t);
                    //        Console.WriteLine("slut");

                    //        //foreach ipaddress in iplist from hostname
                    //        foreach (IPAddress ip in logic.GetIPArrayFromHostName(t))
                    //        {
                    //            //writeout ipaddress as string 
                    //            Console.WriteLine(ip.ToString());
                    //        }
                    //        #endregion
                    //        Console.ReadKey();
                    //        break;
                    //    case 3:
                    //        #region Traceroute
                    //        string a = logic.Traceroute("8.8.8.8");
                    //        Console.WriteLine("route*** " + a);
                    //        #endregion
                    //        Console.ReadKey();
                    //        break;
                    //    case 4:
                    //        #region display dhcp server address from this computers network adapters 
                    //        Console.WriteLine("DHCP Servers");
                    //        //foreach adapters on this computer
                    //        foreach (NetworkInterface adapter in logic.GetNetworkAdapters())
                    //        {
                    //            //write out adapter desciption
                    //            Console.WriteLine(adapter.Description);
                    //            //foreach IPaddress in adapter
                    //            foreach (IPAddress dhcpaddress in logic.GetDHCPServerAddresses(adapter))
                    //            {
                    //                //write out IP-address
                    //                Console.WriteLine("  Dhcp Address ............................ : {0}", dhcpaddress.ToString());
                    //            }
                    //        }

                    //        //Console.WriteLine();
                    //        #endregion
                    //        Console.ReadKey();
                    //        break;
                    //    case 5:
                    //        #region Display hostname ipaddress and aliases option this computer

                    //        //string hostName = "LAPTOP-DELL-PKL";
                    //        //string hostName = "ZBC-RG01203MKC";

                    //        string hostName = Environment.MachineName.ToString();



                    //        //writeout hostname
                    //        Console.WriteLine("Host name : " + hostName);

                    //        Console.WriteLine("\nAliases : ");
                    //        //boolean var to get output from hostentry exception
                    //        bool hostentryex;
                    //        //Foreach alias in hostentry.Aliases write out aliases
                    //        IPHostEntry hostEntry = logic.GetHostInfoFromHostname(hostName, out hostentryex);
                    //        //if exception is true then skip if statement
                    //        if (!hostentryex)
                    //        {
                    //            foreach (string alias in logic.GetAliasesFromHostInfo(hostEntry))
                    //            {
                    //                Console.WriteLine(alias);
                    //            }

                    //            Console.WriteLine("\nIP address list : ");
                    //            //Foreach address in IPHostEntry.AddressList write out ipaddress
                    //            foreach (IPAddress address in logic.GetIpFromHostInfo(hostEntry))
                    //            {
                    //                Console.WriteLine(address);
                    //            }
                    //        }
                    //        Console.ReadKey();
                    //        #endregion
                    //        Console.ReadKey();
                    //        break;
                    //    default:
                    //        break;
                    //}
                }
            }
        }

        public static Queue<string> MakeOutputToStrArray(int option)
        {
            Logic logic = new Logic();

            //Queue to contain all output 
            Queue<string> outputQue = new Queue<string>();

            switch (option)
            {
                case 0:
                    #region Get IP from Hostname
                    string hostname = "en.wikipedia.org";//Console.ReadLine();
                    outputQue.Enqueue($"{hostname}s IP adresses");
                    foreach (IPAddress ip in logic.GetIPArrayFromHostName(hostname))
                    {
                        outputQue.Enqueue(ip.ToString());
                    }
                    #endregion
                    break;
                case 1:
                    #region LocalPing
                    //get local ping reply
                    PingReply localpingReply = logic.LocalPing();
                    //error handling - IF reply is success then write it else write status.
                    if (localpingReply.Status == IPStatus.Success)
                    {
                        outputQue.Enqueue($"Address: {localpingReply.Address}");
                        outputQue.Enqueue($"RoundTrip time: {localpingReply.RoundtripTime}");
                        outputQue.Enqueue($"Time to live: {localpingReply.Options.Ttl}");
                        outputQue.Enqueue($"Don't fragment: {localpingReply.Options.DontFragment}");
                        outputQue.Enqueue($"Buffer size: {localpingReply.Buffer.Length}");
                    }
                    else
                    {
                        outputQue.Enqueue(localpingReply.Status.ToString());
                    }
                    #endregion
                    break;
                case 2:
                    #region Get Hostname From IP and ip from Hostname
                    outputQue.Enqueue("start");
                    string t = logic.GetHostnameFromIp("8.8.8.8");
                    outputQue.Enqueue(t);
                    outputQue.Enqueue("slut");

                    //foreach ipaddress in iplist from hostname
                    foreach (IPAddress ip in logic.GetIPArrayFromHostName(t))
                    {
                        //writeout ipaddress as string 
                        outputQue.Enqueue(ip.ToString());
                    }
                    #endregion
                    break;
                case 3:
                    #region Traceroute
                    string a = logic.Traceroute("8.8.8.8");
                    outputQue.Enqueue("route*** " + a);
                    #endregion
                    break;
                case 4:
                    #region display dhcp server address from this computers network adapters 
                    outputQue.Enqueue("DHCP Servers");
                    //foreach adapters on this computer
                    foreach (NetworkInterface adapter in logic.GetNetworkAdapters())
                    {
                        //write out adapter desciption
                        outputQue.Enqueue(adapter.Description);
                        //foreach IPaddress in adapter
                        foreach (IPAddress dhcpaddress in logic.GetDHCPServerAddresses(adapter))
                        {
                            //write out IP-address
                            outputQue.Enqueue($"  Dhcp Address ............................ : {dhcpaddress}");
                        }
                    }
                    #endregion
                    break;
                case 5:
                    #region Display hostname ipaddress and aliases option this computer

                    //string hostName = "LAPTOP-DELL-PKL";
                    //string hostName = "ZBC-RG01203MKC";

                    string hostName = Environment.MachineName.ToString();

                    //writeout hostname
                    outputQue.Enqueue("Host name : " + hostName);

                    outputQue.Enqueue("\nAliases : ");
                    //boolean var to get output from hostentry exception
                    bool hostentryex;
                    //Foreach alias in hostentry.Aliases write out aliases
                    IPHostEntry hostEntry = logic.GetHostInfoFromHostname(hostName, out hostentryex);
                    //if exception is true then skip if statement
                    if (!hostentryex)
                    {
                        foreach (string alias in logic.GetAliasesFromHostInfo(hostEntry))
                        {
                            outputQue.Enqueue(alias);
                        }

                        outputQue.Enqueue("\nIP address list : ");
                        //Foreach address in IPHostEntry.AddressList write out ipaddress
                        foreach (IPAddress address in logic.GetIpFromHostInfo(hostEntry))
                        {
                            outputQue.Enqueue(address.ToString());
                        }
                    }
                    #endregion
                    break;
                default:
                    break;
            }
            return outputQue;

        }
    }
}
