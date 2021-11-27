using System;

namespace Basic_Management.Commands.CommandExecutors
{
    public class Rand : Command
    {
        Random _random = new Random();
        public Rand() : base("random", AccessType.USER, "I generate random numbers between argument 1 and argument 2", TypeCode.Int32, TypeCode.Int32){}

        public override void RunCommand(string[] args)
        {
            int a = int.Parse(args[0]),b = int.Parse(args[1]);
            Console.WriteLine(_random.Next(0, Math.Abs(a - b) + 1) + Math.Min(a, b));
        }
    }
}