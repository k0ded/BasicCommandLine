using System;

namespace Basic_Management.Commands.CommandExecutors
{
    public class AccurateRandom : Command
    {
        Random _random = new Random();

        public AccurateRandom() : base("Accurand", AccessType.USER, "Adds decimalpoints to the overall randomness",
            TypeCode.Double, TypeCode.Double){}

        public override void RunCommand(string[] args)
        {
            double number1 = double.Parse(args[0]), number2 = double.Parse(args[1]);
            Console.WriteLine(Math.Abs(number1 - number2) * _random.NextDouble() + Math.Min(number1, number2));
        }
    }
}