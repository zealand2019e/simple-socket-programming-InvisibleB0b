using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace echo_client
{
    class Program
    {
        static void Main(string[] args)
        {


            string localAddress = null;
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    Console.WriteLine(ip.ToString());

                    localAddress = ip.ToString();

                }
            }

            Console.ReadKey();

            TcpClient client = new TcpClient("localhost", 3001);

            Stream ns = client.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;

            while (true)
            {


                string message = sr.ReadLine();

                if (message.ToLower().Contains("luk"))
                {
                    break;
                }
                else
                {
                    Console.WriteLine(message);
                    string messag = Console.ReadLine();
                    sw.WriteLine(messag);
                }


            }
        }
    }
}
