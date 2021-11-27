using System;

namespace Basic_Management.Commands.Prompt
{
    public class Prompter : IPrompt
    {
        public string PromptUser(string prompt)
        {
            Console.Write(prompt);
            string s = Console.ReadLine();
            Console.WriteLine();
            return s;
        }
    }
}