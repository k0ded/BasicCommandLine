using System;
using System.Security.Cryptography;
using System.Text;
using Basic_Management.Commands.Prompt;

namespace Basic_Management.Commands
{
    public abstract class SuperGuestCommand : Command
    {
        protected SuperGuestCommand(string baseCommand, string description = "Im a command!", params TypeCode[] codes) : base(baseCommand, AccessType.SUPERGUEST, description, codes){}

        public override FailType CanExecute(params string[] args)
        {
            FailType t = base.CanExecute(args);
            if (t != FailType.NONE)
                return t;
            HiddenPrompter prompter = new HiddenPrompter();
            string password = prompter.PromptUser("Password for root: ");
            return BitConverter.ToString(SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(password))) == Program.A ? FailType.NONE : FailType.PASSWORD;
        }
    }
}