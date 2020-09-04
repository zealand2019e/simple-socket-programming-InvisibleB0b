using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace EchoServer
{
    class Server
    {

        public static void Start()
        {

            try
            {
                TcpListener server = null;

                IPAddress localAddress = null;
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        Console.WriteLine(ip.ToString());

                        localAddress = IPAddress.Parse(ip.ToString());

                    }
                }

                int port = 7777;


                server = new TcpListener(localAddress, port);

                server.Start();

                Console.Write("Waiting for a connection........");

                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Connected!");

                DoClient(client);


                server.Stop();
                Console.WriteLine("server stopped");


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }

        public static void DoClient(TcpClient socket)
        {
            Stream ns = socket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;

            while (true)
            {

                string message = sr.ReadLine();

                Console.WriteLine("Received message : " + message);

                if (message.ToLower() == "luk" || message.ToLower() == "close")
                {

                    sw.WriteLine("You're connection have been terminated");
                    break;
                }
                else if (message != null)
                {
                    sw.WriteLine(message.ToUpper());
                }

            }


            ns.Close();

            Console.WriteLine("net stream closed");

            socket.Close();
            Console.WriteLine("client closed");
        }
    }
}
