using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Basic_Management.Commands.Prompt;
using Basic_Management.Utils;

namespace Basic_Management.Commands.CommandExecutors
{
    public class UserList : Command
    {
        public UserList() : base("users", AccessType.GUEST, "Displays all users!") {}

        public override void RunCommand(string[] args)
        {
            foreach (var n in UserManager.Users.Keys)
            {
                Console.WriteLine(n);
            }
        }
    }
    
    public class UserCommand : Command
    {
        public UserCommand() : base("user", AccessType.SUPERUSER, "Base command for all user related commands"){}
        
        public override void RunCommand(string[] args)
        {
            if (args.Length == 2)
            {
                string baseArg = args[0];
                if (baseArg != "create") return;

                if (File.Exists(Program.location + args[1] + ".bin"))
                {
                    Console.WriteLine("USER ALREADY EXISTS, CHOOSE ANOTHER NAME!");
                    return;
                }

                Console.Write("Password for " + args[1] + ": ");
                StringBuilder sb = new StringBuilder();
                while (true)
                {
                    ConsoleKeyInfo c = Console.ReadKey(true);

                    if (c.Key == ConsoleKey.Enter) break;
                    if (c.Key == ConsoleKey.Backspace) sb.Remove(sb.Length - 1, 1);
                    else
                    {
                        sb.Append(c.KeyChar);
                    }
                }

                Console.WriteLine();

                User u = new User(args[1], sb.ToString());
                UserManager.SaveUser(u);
            }else if (args.Length == 3)
            {
                string username = args[0];
                string action = args[1];
                string value = args[2];

                if (UserManager.Users.ContainsKey(username))
                {
                    User user = UserManager.Users[username];
                    
                    HiddenPrompter hiddenPrompter = new HiddenPrompter();
                    Prompter prompter = new Prompter();
                    
                    string password = hiddenPrompter.PromptUser($"Please enter the password for {user.AccountName}: ");
                    if (!user.CheckPassword(password))
                    {
                        Console.WriteLine("Wrong password!");
                        return;
                    }
                    
                    switch (action.ToLower())
                    {
                        case "set":
                            switch (value.ToLower())
                            {
                                case "password":
                                    user.ChangePasswordTo(
                                        hiddenPrompter.PromptUser($"Please enter the new password for {user.AccountName}: "));
                                    break;
                                case "username":
                                    user.ChangeUsernameTo(prompter.PromptUser($"Please enter the new username for {user.AccountName} "));
                                    break;
                                case "access":
                                    int i = 0;
                                    foreach (var access in Enum.GetNames(typeof(AccessType)))
                                    {
                                        if (i == 0)
                                            i++;
                                        else
                                            Console.WriteLine(access + " >> " + i++);
                                    }
                                    
                                    NumberPrompter numberPrompter = new NumberPrompter(1, i);
                                    string input = numberPrompter.PromptUser("Please choose a AccessType: ");
                                    if (input != "")
                                    {
                                        user.ChangeAccessTo((AccessType) int.Parse(input));
                                    }
                                    break;
                            }
                            break;
                        case "read":
                            switch (value.ToLower())
                            {
                                case "password":
                                    Console.WriteLine("Cannot read password values");
                                    break;
                                case "access":
                                    Console.WriteLine();
                                    break;
                            }
                            break;
                    }
                }else
                    Console.WriteLine("User not found! Try creating one first by doing \"user create <username>\"");
            }
        }
    }
}