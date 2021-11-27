using System;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using Basic_Management.Commands;
using Basic_Management.Utils;

namespace Basic_Management
{
    class Program
    {
        public static CommandHandler handler;
        public static string A = BitConverter.ToString(SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes("toor")));
        public static string location = @"C:\Users\Liam\Documents\Console\";

        static void Main(string[] args)
        {
            UpdateLoggedIn(new TempUser());
            AssemblyName currentAssembly = AssemblyName.GetAssemblyName(Assembly.GetExecutingAssembly().Location);
            Version assemblyVersion = currentAssembly.Version;
            Console.WriteLine($"LSBash [Version: {assemblyVersion}]");
            UserManager.LoadUsers();
            handler = new CommandHandler();
        }

        public static void UpdateLoggedIn(User user)
        {
            UserManager.LoggedIn = user;
            Console.Title = $"LSBash [{UserManager.LoggedIn.AccountName} - {UserManager.LoggedIn.AccessType}]";
        }
    }

    internal class TempUser : User
    {
        public TempUser() : base("temp", "_")
        {
            AccessType = AccessType.GUEST;
        }
    }
}