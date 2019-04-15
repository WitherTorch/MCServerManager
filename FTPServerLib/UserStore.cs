using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace FTPServerLib
{
    public static class UserStore
    {
        private static List<User> _users;

        static UserStore()
        {
            _users = new List<User>();

            XmlSerializer serializer = new XmlSerializer(_users.GetType(), new XmlRootAttribute("Users"));

            if (File.Exists("accounts.xml"))
            {
                _users = serializer.Deserialize(new StreamReader("accounts.xml")) as List<User>;
            }
            else
            {
                using (StreamWriter w = new StreamWriter("accounts.xml"))
                {
                    serializer.Serialize(w, _users);
                }
            }
        }
        public static User AddUser(string username, string password)
        {

           User  _currentUser = Validate(username, password);
            if (_currentUser == null)
            {
                User user = new User()
                {
                    Username = username,
                    Password = password,
                };
                _users.Add(user);
                _currentUser = user;
            }
            return _currentUser;
        }
        public static void RemoveUser(string username, string password)
        {

            User _currentUser = Validate(username, password);
            if (_currentUser != null)
            {
                _users.Remove(_currentUser);
            }
        }
        public static User Validate(string username, string password)
        {
            User user = (from u in _users where u.Username == username && u.Password == password select u).SingleOrDefault();
            return user;
        }
    }
}
