using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Transactions;

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

                int port = 3001;


                server = new TcpListener(localAddress, port);

                server.Start();

                Console.WriteLine("Waiting for a connection........");

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

            sw.WriteLine("You are connected!!!");

            while (true)
            {



                string message = sr.ReadLine();

                if (message.ToLower().Contains("luk"))
                {
                    break;
                }

                Console.WriteLine("Received message : " + message);

                double first = Convert.ToDouble(message.Split(" ")[0]);

                double secound = Convert.ToDouble(message.Split(" ")[1]);

                sw.WriteLine($"result : {first + secound}");



            }
            sw.WriteLine("Luk");

            ns.Close();

            Console.WriteLine("net stream closed");

            socket.Close();
            Console.WriteLine("client closed");
        }
    }
}
