using System;
using Basic_Management.Commands.Prompt;
using Basic_Management.Utils;

namespace Basic_Management.Commands.CommandExecutors
{
    public class Login : Command
    {
        public Login() : base("login", AccessType.GUEST, "Logs you into your account.", TypeCode.String){}

        public override void RunCommand(string[] args)
        {
            string username = args[0];
            HiddenPrompter prompter = new HiddenPrompter();

            if (!UserManager.Users.ContainsKey(username))
            {
                Console.WriteLine("User doesn't exist, sudo user and create one!");
                return;
            }
            
            string password = prompter.PromptUser($"Enter password for {args[0]}: ");
            User u = UserManager.Users[username];

            if (u.CheckPassword(password))
                Program.UpdateLoggedIn(u);
            else
                Console.WriteLine("Wrong password!");
        }
    }
}