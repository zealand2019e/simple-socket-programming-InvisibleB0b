using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
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


                server = new TcpListener(IPAddress.Loopback, port);

                server.Start();

                Console.WriteLine("Waiting for a connection........");




                while (true)
                {


                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    Task.Run(() =>
                    {
                        TcpClient tempSocket = client;
                        DoClient(tempSocket);
                    });


                }

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
            int numOfWords = 0;

            while (true)
            {



                string message = sr.ReadLine();

                if (message.ToLower().Contains("luk"))
                {
                    break;
                }

                numOfWords += message.Split(" ").Length;

                Console.WriteLine("Received message : " + message);


                Thread.Sleep(5000);

                sw.WriteLine(message + $" number of send words {numOfWords}");


                //double first = Convert.ToDouble(message.Split(" ")[0]);

                //double secound = Convert.ToDouble(message.Split(" ")[1]);

                //string third = message.Split(" ")[2].ToUpper();

                //switch (third)
                //{
                //    case "SUB":
                //        sw.WriteLine($"result : {first - secound} Number of words = {numOfWords}");
                //        break;
                //    case "PLU":
                //        sw.WriteLine($"result : {first + secound} Number of words = {numOfWords}");
                //        break;
                //    case "MUL":
                //        sw.WriteLine($"result : {first * secound} Number of words = {numOfWords}");
                //        break;
                //    case "DEV":
                //        sw.WriteLine($"result : {first / secound} Number of words = {numOfWords}");
                //        break;
                //    default:
                //        sw.WriteLine("Not a function");
                //        break;
                //}




            }
            sw.WriteLine("Luk");

            ns.Close();

            Console.WriteLine("net stream closed");

            socket.Close();
            Console.WriteLine("client closed");
        }
    }
}
