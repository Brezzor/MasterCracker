using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPserver.model
{
    /// <summary>
    /// Username + password in clear text (that is human readable)
    /// </summary>
    internal class UserInfoClearText
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public UserInfoClearText(string username, string password)
        {
            if (username == null)
            {
                throw new ArgumentNullException("username");
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            UserName = username;
            Password = password;
        }

        public override string ToString()
        {
            return UserName + ": " + Password;
        }
    }
}
