using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MasterCracker.model;
using MasterCracker.handlers;

namespace MasterCracker
{
    internal class Server
    {

        public static void StartServer()
        {
            Console.WriteLine("Starting server...");

            var endPoint = new IPEndPoint(IPAddress.Any, 13);
            TcpListener listener = new TcpListener(endPoint);

            listener.Start();
            Console.WriteLine("Server started");

            Console.WriteLine("Listening for clients");
            Console.WriteLine($"IpAddress: {endPoint.Address}");
            Console.WriteLine($"Port: {endPoint.Port}");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine($"{client.Client.RemoteEndPoint}: connected!");

                try
                {
                    Thread newClient = new Thread(() => ClientHandler.HandleClient(client));
                    newClient.Start();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
            }
        }
    }
}
