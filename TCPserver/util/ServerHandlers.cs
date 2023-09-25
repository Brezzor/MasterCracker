using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPserver.util
{
    internal class ServerHandlers
    {
        static bool keepRunning = true;
        public static void HandleClient(TcpClient client)
        {

            NetworkStream ns = client.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns)
            {
                AutoFlush = true
            };

            while (keepRunning)
            {
                try
                {
                    HandleClientRequest(sr, sw);
                }
                catch (Exception ex)
                {
                    keepRunning = false;
                    Console.WriteLine(ex.Message);
                }
            }
            ns.Close();
        }

        static void HandleClientRequest(StreamReader streamReader, StreamWriter streamWriter)
        {
            var request = streamReader.ReadLine();

            switch (request)
            {
                case "New password":
                    Console.WriteLine("do something");
                    break;
                default:
                    break;
            }
        }
    }
}
