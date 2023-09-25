using MasterCracker.model;
using MasterCracker.repos;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MasterCracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserInfosRepo.Initialize();
            ChunksRepo.Initialize();
            Server.StartServer();
            Console.ReadKey();
        }
    }
}