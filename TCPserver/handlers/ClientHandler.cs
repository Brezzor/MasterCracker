using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MasterCracker.handlers
{
    internal class ClientHandler
    {
        static bool keepRunning = true;
        static NetworkStream ns;
        static StreamReader sr;
        static StreamWriter sw;

        public static async Task HandleClient(TcpClient client)
        {
            ns = client.GetStream();
            sr = new StreamReader(ns);
            sw = new StreamWriter(ns)
            {
                AutoFlush = true
            };

            while (keepRunning)
            {
                try
                {
                    await HandleClientRequest();
                }
                catch (Exception ex)
                {
                    keepRunning = false;
                    Console.WriteLine(ex.Message);
                }
            }
            ns.Close();
        }

        static async Task HandleClientRequest()
        {
            var request = await sr.ReadLineAsync();

            switch (request)
            {
                case "New password":
                    sw.WriteLine();
                    break;

                case "New chunk":
                    sw.WriteLine();
                    break;

                default:
                    break;
            }
        }


    }
}
