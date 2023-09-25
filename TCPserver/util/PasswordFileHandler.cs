using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TCPserver.model;

namespace TCPserver.util
{
    internal class PasswordFileHandler
    {
        private static readonly Converter<char, byte> Converter = CharToByte;

        /// <summary>
        /// Reads all the username + encrypted password from the password file
        /// </summary>
        /// <param name="filename">the name of the password file</param>
        /// <returns>A list of (username, encrypted password) pairs</returns>
        public static List<UserInfo> ReadPasswordFile(String filename)
        {
            List<UserInfo> result = new List<UserInfo>();

            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using (StreamReader sr = new StreamReader(fs))
            {

                while (!sr.EndOfStream)
                {
                    string? line = sr.ReadLine();
                    string[] parts = line!.Split(":".ToCharArray());
                    UserInfo userInfo = new UserInfo(parts[0], parts[1]);
                    result.Add(userInfo);
                }
                return result;
            }
        }

        public static Converter<char, byte> GetConverter()
        {
            return Converter;
        }

        /// <summary>
        /// Converting a char to a byte can be done in many ways.
        /// This is one way ...
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        private static byte CharToByte(char ch)
        {
            return Convert.ToByte(ch);
        }
    }
}
