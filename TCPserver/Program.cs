using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCPserver
{
    internal class Program
    {
        static async void Main(string[] args)
        {
            var ipEndPoint = new IPEndPoint(IPAddress.Any, 13);
            TcpListener listener = new TcpListener(ipEndPoint);

            try
            {
                listener.Start();

                using TcpClient handler = await listener.AcceptTcpClientAsync();
                await using NetworkStream stream = handler.GetStream();
                
                var message = $"{DateTime.Now}";
                var dateTimeBytes = Encoding.UTF8.GetBytes(message);
                await stream.WriteAsync(dateTimeBytes);

                Console.WriteLine($"Sent message: \"{message}\"");
            }
            finally
            {
                listener.Stop();
            }
        }
    }
}