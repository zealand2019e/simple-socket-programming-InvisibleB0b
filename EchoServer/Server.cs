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

            sw.WriteLine("You are connected!!!");

            while (true)
            {

                sw.WriteLine("enter first numb");

                string firstLine = sr.ReadLine();

                if (firstLine.ToLower().Contains("luk"))
                {
                    break;
                }

                Console.WriteLine("Received message : " + firstLine);

                sw.WriteLine("enter secound number");

                string secoundLine = sr.ReadLine();

                Console.WriteLine("Received message : " + secoundLine);

                double result = Convert.ToDouble(firstLine) + Convert.ToDouble(secoundLine);

                sw.WriteLine($"Your result is {result}");

                //if (message.ToLower().Contains("luk") || message.ToLower().Contains("close"))
                //{

                //    sw.WriteLine("You're connection have been terminated");
                //    break;
                //}
                //else if (message != null)
                //{
                //    sw.WriteLine(message.ToUpper());
                //}



            }


            ns.Close();

            Console.WriteLine("net stream closed");

            socket.Close();
            Console.WriteLine("client closed");
        }
    }
}
