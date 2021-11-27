using System;

namespace Basic_Management.Commands.Prompt
{
    public class NumberPrompter : IPrompt
    {
        private int lowBound = 0;
        private int highBound = 0;
        private string failed;
        
        /// <summary>
        /// Initializes the NumberPrompter with the different boundaries
        /// set by <paramref name="bound1"/> and <paramref name="bound2"/>.
        ///
        /// When the given input is not a number it will be replaced with a failed message.
        /// %given% will be replaced by the given input in a string value
        /// %low% will be replaced by the low bound of the instance.
        /// %high% will be replaced by the high bound of the instance.
        /// </summary>
        /// <param name="bound1"></param>
        /// <param name="bound2"></param>
        /// <param name="failedMessage"></param>
        public NumberPrompter(int bound1, int bound2, string failedMessage = "%given% is either not a number or its not in the bounds of %low%-%high%. ")
        {
            lowBound = Math.Min(bound1, bound2);
            highBound = Math.Max(bound1, bound2);
            failed = failedMessage;
        }

        public string PromptUser(string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            int choice;
            if (!int.TryParse(input, out choice) || choice > highBound || choice < lowBound)
            {
                Console.WriteLine(TransformFailedMessage(input));
                return "";
            }

            return choice.ToString();
        }

        private string TransformFailedMessage(string s)
        {
            string sf = failed
                .Replace("%given%", s)
                .Replace("%low%", lowBound.ToString())
                .Replace("%high%", highBound.ToString());
            return sf;
        }
    }
}