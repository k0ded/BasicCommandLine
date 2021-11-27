using System;
using System.Linq;
using Basic_Management.Utils;

namespace Basic_Management.Commands.CommandExecutors
{
    public class Help : Command
    {
        public override void RunCommand(string[] s)
        {
            foreach (var command in CommandLine.Instance.Commands.Keys.Where(command => UserManager.LoggedIn.CanAccess(CommandLine.Instance.Commands[command].AccessType)))
            {
                Console.WriteLine(command.ToUpper() + " - " + CommandLine.Instance.Commands[command].Description);
            }
        }

        public Help() : base("help", AccessType.GUEST, "I'm the help command, I show you what you can do!") {}
    }
}