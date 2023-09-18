using System.Net;
using System.Net.Sockets;

namespace TCPserver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InitSocket();
        }

        private static void InitSocket()
        {
            Console.WriteLine("Initializing socket server...");

            TcpListener listener = new TcpListener(IPAddress.Any, 7);
            listener.Start();

            while (true)
            {
                TcpClient socket = listener.AcceptTcpClient();
                Task.Run(() =>
                {
                    HandleClient(socket);
                });
            }

            listener.Stop();
        }

        private static void HandleClient(TcpClient socket)
        {
            Console.WriteLine(socket.Client.RemoteEndPoint?.ToString());

            NetworkStream ns = socket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);

            string message = null;
            while (message == null || !message.Equals("end", StringComparison.OrdinalIgnoreCase))
            {
                message = sr.ReadLine()!;
                Console.WriteLine("Client sent: " + message);

                sw.WriteLine(message);
                sw.Flush();
            }

            socket.Close();
        }
    }
}