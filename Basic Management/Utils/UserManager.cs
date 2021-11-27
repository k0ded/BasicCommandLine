using System.Collections.Generic;
using System.IO;

namespace Basic_Management.Utils
{
    public class UserManager
    {
        public static Dictionary<string, User> Users = new Dictionary<string, User>();
        public static User LoggedIn { get; set; }

        public static void SaveUser(User user)
        {
            if(!Users.ContainsKey(user.AccountName))
                Users.Add(user.AccountName, user);
            FileManager.SaveSerializable(Program.location + user.AccountName + ".user", user);
        }

        public static void DeleteUser(User user)
        {
            if (!Users.ContainsKey(user.AccountName)) return;
            Users.Remove(user.AccountName);
            File.Delete(Program.location + user.AccountName + ".user");
        }
        
        public static void LoadUsers()
        {
            string[] userFiles = Directory.GetFiles(Program.location, "*.user");
            foreach (var filepath in userFiles)
            {
                User u = (User) FileManager.GetSerializable(filepath);
                Users.Add(u.AccountName, u);
            }
        }
    }
}