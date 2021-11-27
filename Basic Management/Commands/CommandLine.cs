using System;
using System.Collections.Generic;
using System.Linq;
using Basic_Management.Commands.Exceptions;
using Basic_Management.Utils;

namespace Basic_Management.Commands
{
    public class CommandLine
    {
        public Dictionary<string, Command> Commands = new Dictionary<string, Command>();
        public static CommandLine Instance;
        
        public CommandLine()
        {
            if (Instance == null)
                Instance = this;
        }

        private bool _cliExists;
        
        /// <summary>
        /// Starts the commandline.
        /// </summary>
        public void StartCommandLine()
        {
            if (_cliExists) return;
            _cliExists = true;
            RunCommandLine();
        }

        /// <summary>
        /// Runs the commandline.
        /// Ignores any calls that get sent when an instance already exists.
        /// </summary>
        private void RunCommandLine()
        {
            Console.WriteLine();
            Console.Write(UserManager.LoggedIn.AccountName + " > ");
            string s = Console.ReadLine();
            if (s == null)
                return;
            string[] input = s.Split(' ');
            string command = input[0];
            string[] args = new string[input.Length - 1];
            Array.Copy(input, 1, args, 0, input.Length - 1);

            ProcessCommand(command, args);
            RunCommandLine();
        }

        /// <summary>
        /// Registers the command so the commandline can find and process it later.
        /// </summary>
        /// <param name="baseCommand">
        /// The base command is the first keyword that points to a specific command such as "help"
        /// </param>
        /// <param name="command">
        /// The command is what holds the code that will be executed upon calling, this may be any calls, even with no params
        /// </param>
        /// <exception cref="CommandRegistrationException">
        /// If the registration of a command is invalid this exception will be raised.
        /// A base command should never:
        ///  - Already be used
        ///  - Have a space or newline
        /// A command should never have two different baseCommands!
        /// </exception>
        public void RegisterCommand(Command command)
        {
            var baseCommand = command.BaseCommand;
            if (Commands.ContainsKey(baseCommand) 
                || Commands.ContainsValue(command) 
                || baseCommand.Contains(" ")
                || baseCommand.Contains("\n"))
                throw new CommandRegistrationException(command.GetType());
            Commands.Add(baseCommand.ToLower(), command);
        }
        
        /// <summary>
        /// Finds the correct command and executes it with the correct arguments
        /// </summary>
        private void ProcessCommand(string command, string[] args)
        {
            string cmd = command.ToLower();
            if (Commands.ContainsKey(cmd))
            {
                FailType t = Commands[cmd].CanExecute(args);
                switch (t)
                {
                    case FailType.NONE:
                        Commands[cmd].RunCommand(args);
                        break;
                    case FailType.ARGUMENT:
                        string output = Commands[cmd].Codes.Aggregate(command, (current, tc) => current + (" " + tc));
                        Console.WriteLine("Command arguments are wrong! Try \"" + output + "\"");
                        break;
                    case FailType.PASSWORD:
                        Console.WriteLine("Password is incorrect! Try again!");
                        break;
                    case FailType.PERMISSION:
                        Console.WriteLine("You don't have permission to execute this command!");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
                Console.WriteLine("Invalid command! Try \"help\".");
        }
    }
}