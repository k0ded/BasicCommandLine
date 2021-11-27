using System;

namespace Basic_Management.Commands.CommandExecutors
{
    public class Exit : Command
    {
        public Exit() : base("exit", AccessType.GUEST, "Exits the application!"){}

        public override void RunCommand(string[] args)
        {
            // Clean up things that uses io etc.
            Environment.Exit(0);
        }
    }
}