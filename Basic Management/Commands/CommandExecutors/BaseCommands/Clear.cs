using System;

namespace Basic_Management.Commands.CommandExecutors
{
    public class Clear : Command
    {
        public Clear() : base("clear", AccessType.GUEST, "Clears the console") {}

        public override void RunCommand(string[] args)
        {
            Console.Clear();
        }
    }
}