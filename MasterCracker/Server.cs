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
        private List<Task> _clients = new List<Task>();
        private bool _keepRunnning = true;

        public async Task StartServerAsync()
        {
            Console.WriteLine("Starting server...");

            var endPoint = new IPEndPoint(IPAddress.Any, 13);
            TcpListener tcpListener = new TcpListener(endPoint);
            tcpListener.Start();

            await ListenForClientsAsync(tcpListener);
        }

        private async Task ListenForClientsAsync(TcpListener tcpListener)
        {
            Console.WriteLine("Listening for clients");

            while (_keepRunnning)
            {
                NumberOfClients();
                TcpClient client = await tcpListener.AcceptTcpClientAsync();
                _clients.Add(Task.Run(() => RunClientAsync(client)));
                _clients.RemoveAll(task => task.IsCompleted);
            }
        }

        private async Task RunClientAsync(TcpClient tcpClient)
        {
            EndPoint? _remoteEndPoint = tcpClient.Client.RemoteEndPoint;

            Console.WriteLine($"{_remoteEndPoint}: connected!");

            Stream stream = tcpClient.GetStream();
            StreamReader streamReader = new StreamReader(stream);
            StreamWriter streamWriter = new StreamWriter(stream)
            { AutoFlush = true };

            while (tcpClient.Connected)
            {
                try
                {
                    string? message = await streamReader.ReadLineAsync();
                    if (message != "" && message != null)
                    {
                        Console.WriteLine($"{_remoteEndPoint}: {message}");
                    }
                    await streamWriter.WriteLineAsync($"Sent: {message}");
                }
                catch (IOException)
                {
                    Console.WriteLine($"{_remoteEndPoint}: Disconnected!");
                    tcpClient.Close();
                }
            }
        }

        private void NumberOfClients()
        {
            Console.WriteLine($"Number of clients: {_clients.Count}");
        }
    }
}
