using System;
using System.Security.Cryptography;
using System.Text;
using Basic_Management.Utils;

namespace Basic_Management
{
    [Serializable]
    public class User
    {
        public AccessType AccessType;
        public string AccountName { get; set; }
        private string password;

        public User(string name, string pass)
        {
            AccountName = name;
            password = BitConverter.ToString(SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(pass)));
            AccessType = AccessType.USER;
        }

        public bool CanAccess(AccessType type)
        {
            return (int) type <= (int) AccessType;
        }

        public bool CheckPassword(string s)
        {
            return password == BitConverter.ToString(SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(s)));
        }

        public void ChangePasswordTo(string pass)
        {
            password = BitConverter.ToString(SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(pass)));
            UserManager.SaveUser(this);
        }

        public void ChangeUsernameTo(string username)
        {
            if (!(MemberwiseClone() is User u)) return;
            if (username.Contains(" ")) return;
            if (UserManager.Users.ContainsKey(username))
            {
                Console.WriteLine("User already exists!");
                return;
            }
            u.AccountName = username;
            UserManager.SaveUser(u);
            UserManager.LoggedIn = u;
            UserManager.DeleteUser(this);
        }

        public void ChangeAccessTo(AccessType type)
        {
            AccessType = type;
            UserManager.SaveUser(this);
        }
    }
}