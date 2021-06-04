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
    class Logic
    {
        public IPAddress[] GetIPArrayFromHostName(string hostname)
        {
            //return all IP address from hostname
            return Dns.GetHostAddresses(hostname);
        }
        
        public PingReply LocalPing()
        {
            // Ping's the local machine.
            Ping pingSender = new Ping();
            //ipaddress to loopback address (127.0.0.1)
            IPAddress address = IPAddress.Loopback;
            PingReply reply = pingSender.Send(address);

            return reply;
        }

        public string GetHostnameFromIp(string Ip)
        {
            string hostname; //create hostname so I can use it in try catch

            try
            {
                IPHostEntry ipHostEntry = Dns.GetHostByAddress(Ip); //get DNS from ipaddress
                hostname = ipHostEntry.HostName;
            }
            catch (FormatException)
            {
                hostname = "Please specify a valid IP address.";
                return hostname;
            }
            catch (SocketException)
            {
                hostname = "Unable to perform lookup - a socket error occured.";
                return hostname;
            }
            catch (SecurityException)
            {
                hostname = "Unable to perform lookup - permission denied.";
                return hostname;
            }
            catch (Exception)
            {
                hostname = "An unspecified error occured.";
                return hostname;
            }

            return hostname;
        }

        public string Traceroute(string ipAddressOrHostName)
        {
            IPAddress ipAddress = Dns.GetHostEntry(ipAddressOrHostName).AddressList[0];
            StringBuilder traceResults = new StringBuilder();


            using (Ping pingSender = new Ping())
            {

                PingOptions pingOptions = new PingOptions();
                Stopwatch stopWatch = new Stopwatch();
                byte[] bytes = new byte[32];

                pingOptions.DontFragment = true;
                pingOptions.Ttl = 1;
                int maxHops = 30;

                traceResults.AppendLine(
                    string.Format(
                        "Tracing route to {0} over a maximum of {1} hops:",
                        ipAddress,
                        maxHops));

                traceResults.AppendLine();

                for (int i = 1; i < maxHops + 1; i++)
                {
                    stopWatch.Reset();
                    stopWatch.Start();

                    PingReply pingReply = pingSender.Send(
                        ipAddress,
                        5000,
                        new byte[32], pingOptions);

                    stopWatch.Stop();

                    traceResults.AppendLine(
                        string.Format("{0}\t{1} ms\t{2}",
                        i,
                        stopWatch.ElapsedMilliseconds,
                        pingReply.Address));



                    if (pingReply.Status == IPStatus.Success)
                    {
                        traceResults.AppendLine();
                        traceResults.AppendLine("Trace complete.");
                        break;
                    }

                    pingOptions.Ttl++;

                }
            }
            return traceResults.ToString();
        }

        public NetworkInterface[] GetThisComputersAdapters()
        {
            //return all network adapters from this computer
            return NetworkInterface.GetAllNetworkInterfaces();
        }
        
        public IPAddressCollection GetDHCPServerAddresses(NetworkInterface adapter)
        {
            //Get adapter properties
            IPInterfaceProperties adapteradapterProperties = adapter.GetIPProperties();
            //Get ipaddresses from adapterproperties 
            IPAddressCollection addresses = adapteradapterProperties.DhcpServerAddresses;
            //If there are any addresses
            if (addresses.Count > 0)
            {
                return addresses;
            }

            //TODO: error handling
            return addresses;
        }

        public IPHostEntry GetHostInfoFromHostname(string hostName, out bool exception)
        {
            IPHostEntry hostEntry = null;
            try
            {
                hostEntry = Dns.GetHostByName(hostName); //get DNS information from hostname
                exception = false;

            }
            catch (SocketException) //error handling 
            {
                Console.WriteLine("Fangede fejlen");
                exception = true;
            }
            catch (Exception)
            {
                throw;
            }
            return hostEntry;
        }

        public IPAddress[] GetIpFromHostInfo(IPHostEntry hostEntry)
        {
            return hostEntry.AddressList;
        }

        public string[] GetAliasesFromHostInfo(IPHostEntry hostEntry)
        {
            // Get the alias names of the addresses in the IP address list.
            return hostEntry.Aliases;
        }
    }
}
