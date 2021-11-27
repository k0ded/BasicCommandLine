using System;
using System.Linq;
using System.Reflection;

namespace Basic_Management.Commands
{
    public class CommandHandler
    {
        public CommandHandler()
        {
            var cmdLine = new CommandLine();
            RegisterCommands();
            cmdLine.StartCommandLine();
        }

        /// <summary>
        /// Registers all commands in the CommandExecutors Namespace
        /// </summary>
        private void RegisterCommands()
        {
            // Creates a list of types within the namespace.
            var commands = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.Namespace == "Basic_Management.Commands.CommandExecutors")
                .ToArray();

            // Performs RegisterCommand on each of the new instances
            foreach (var t in commands)
            {
                if (!t.IsSubclassOf(typeof(Command))) continue;
                CommandLine.Instance.RegisterCommand((Command) Activator.CreateInstance(t));
            }
        }
    }
}