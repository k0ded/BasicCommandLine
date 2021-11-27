using System;
using System.Text;

namespace Basic_Management.Commands.Prompt
{
    public class HiddenPrompter : IPrompt
    {
        public string PromptUser(string prompt)
        {
            Console.Write(prompt);
            var sb = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo c = Console.ReadKey(true);

                if (c.Key == ConsoleKey.Enter) break;
                if (c.Key == ConsoleKey.Backspace)
                    if(sb.Length > 0)
                        sb.Remove(sb.Length - 1, 1);
                    else 
                        continue;
                sb.Append(c.KeyChar);
            }
            Console.WriteLine();
            return sb.ToString();
        }
    }
}