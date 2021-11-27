using System;
using System.Linq;

namespace Basic_Management.Commands.Exceptions
{
    public class CommandRegistrationException : Exception
    {
        public CommandRegistrationException(Type command)
        {
            Console.WriteLine(command.FullName + " has issues registering itself. This may be because it isn't extending the Command class or the BaseCommand is invalid!");
        }
    }
}