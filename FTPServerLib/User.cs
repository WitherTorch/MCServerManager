using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FTPServerLib
{
    [Serializable]
    public class User
    {
        [XmlAttribute("username")]
        public string Username { get; set; }

        [XmlAttribute("password")]
        public string Password { get; set; }

        public string HomeDir
        {
            get
            {
                return System.IO.Path.Combine(FtpServer.homeDir, @"\", Username);
            }
           private set
            {
            }
        }

    }
}
