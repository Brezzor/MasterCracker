using MasterCracker.handlers;
using MasterCracker.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCracker.repos
{
    internal class UserInfosRepo
    {
        private static int UserInfoIndex = 0;
        private static List<UserInfo> _userInfos = new List<UserInfo>();

        public static void Initialize()
        {
            Console.WriteLine("Reading passwords from file");
            _userInfos = PasswordFileHandler.ReadPasswordFile("passwords.txt");
            Console.WriteLine("Passwords read");
        }

        public static UserInfo? RequestUserInfo()
        {
            UserInfo? userInfo = _userInfos[UserInfoIndex];
            if (userInfo == null)
            {
                return null;
            }
            UserInfoIndex++;
            return userInfo;
        }
    }
}
