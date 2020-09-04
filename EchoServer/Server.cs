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
            TcpListener server = null;
            try
            {

            int port = 7777;
            IPAddress localAddress = IPAddress.Loopback;

            server = new TcpListener(localAddress, port);

            server.Start();

                Console.Write("Waiting for a connection........");

                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Connected!");

                Stream ns = client.GetStream();
                StreamReader sr = new StreamReader(ns);
                StreamWriter sw = new StreamWriter(ns);
                sw.AutoFlush = true;

                while (true)
                {

                    string message = sr.ReadLine();

                    Console.WriteLine("Received message : " + message);

                    if ( message.ToLower() == "luk" || message.ToLower() == "close")
                    {

                        sw.WriteLine("You're connection have been terminated");
                        break;
                    }
                    if (message != null)
                    {
                        sw.WriteLine(message.ToUpper());
                    }

                   
                }

             
                ns.Close();

                Console.WriteLine("net stream closed");

                client.Close();
                Console.WriteLine("client closed");

                server.Stop();
                Console.WriteLine("server stopped");


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }
    }
}
