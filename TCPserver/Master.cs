using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TCPserver.model;
using TCPserver.util;

namespace TCPserver
{
    internal class Master
    {
        private static List<UserInfo> _userInfos = new List<UserInfo>();
        private static List<string> _dictionaryEnteries = new List<string>();
        private static List<string[]> _chunks = new List<string[]>();

        public static void Start()
        {
            Console.WriteLine("Starting master cracker");
            InitializePasswords();
            InitializeDictonary();
            CreateChunks();
            StartServer();
        }

        static void InitializePasswords()
        {
            Console.WriteLine("Reading passwords from file");
            _userInfos = PasswordFileHandler.ReadPasswordFile("passwords.txt");
            Console.WriteLine("Passwords read");
        }

        static void InitializeDictonary()
        {
            Console.WriteLine("Reading dictonary enteries");
            using (FileStream fs = new FileStream("webster-dictionary.txt", FileMode.Open, FileAccess.Read))
            using (StreamReader sr = new StreamReader(fs))
            {
                while (!sr.EndOfStream)
                {
                    string? dictionaryEntry = sr.ReadLine();
                    _dictionaryEnteries.Add(dictionaryEntry!);
                }
            }
            Console.WriteLine("Dictonary enteries read");
        }

        static void CreateChunks()
        {
            Console.WriteLine("Creating chunks from dictonary enteries");
            _chunks = _dictionaryEnteries.Chunk(10000).ToList();
            Console.WriteLine("Chunks created");
        }

        static void StartServer()
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
                    Thread newClient = new Thread(() => ServerHandlers.HandleClient(client));
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
