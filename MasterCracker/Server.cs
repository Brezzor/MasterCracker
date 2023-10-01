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
        private static List<Task> Tasks = new List<Task>();
        private static bool _keepRunnning = true;

        public static async Task StartServerAsync()
        {
            Console.WriteLine("Starting server...");

            var endPoint = new IPEndPoint(IPAddress.Any, 13);
            TcpListener tcpListener = new TcpListener(endPoint);
            tcpListener.Start();

            await ListenForClientsAsync(tcpListener);
        }

        private static async Task ListenForClientsAsync(TcpListener tcpListener)
        {            
            Console.WriteLine("Listening for clients");

            while (_keepRunnning)
            {
                TcpClient client = await tcpListener.AcceptTcpClientAsync();
                Tasks.Add(Task.Run(() => RunClientAsync(client)));
            }
        }

        private static async Task RunClientAsync(TcpClient tcpClient)
        {
            Console.WriteLine($"{tcpClient.Client.RemoteEndPoint}: connected!");
            
            Stream stream = tcpClient.GetStream();
            StreamReader streamReader = new StreamReader(stream);
            StreamWriter streamWriter = new StreamWriter(stream);

            while (_keepRunnning)
            {
                var message = await streamReader.ReadLineAsync();
                Console.WriteLine(message);
                await streamWriter.WriteLineAsync(message);
            }
        }
    }
}
