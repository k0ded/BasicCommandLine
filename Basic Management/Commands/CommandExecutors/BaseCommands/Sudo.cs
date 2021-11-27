using System;

namespace Basic_Management.Commands.CommandExecutors
{
    public class Sudo : SuperGuestCommand
    {
        public Sudo() : base("sudo", "Executes a command as root") {}

        public override void RunCommand(string[] args)
        {
            if (args.Length < 1) return;
            string baseCmd = args[0];
            string[] arg = new string[args.Length - 1];
            Array.Copy(args, 1, arg, 0, args.Length - 1);
            if(CommandLine.Instance.Commands.ContainsKey(baseCmd))
                CommandLine.Instance.Commands[baseCmd].RunCommand(arg);
            else
                Console.WriteLine("Command not found!");
        }
    }
}