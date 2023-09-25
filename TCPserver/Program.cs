using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCPserver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Master.Start();
            Console.ReadKey();
        }
    }
}