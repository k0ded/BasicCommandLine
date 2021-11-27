using System;

namespace Basic_Management.Commands.CommandExecutors
{
    public class Echo : Command
    {
        public Echo() : base("echo", AccessType.USER, "Copies your message arg1, arg2 amount of times", TypeCode.String, TypeCode.UInt32){}

        public override void RunCommand(string[] args)
        {
            for (int i = 0; i < UInt32.Parse(args[1]); i++)
            {
                Console.WriteLine(args[0]);
            }
        }
    }
}