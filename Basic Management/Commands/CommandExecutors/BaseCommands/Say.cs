using System;
using System.Speech.Synthesis;
using System.Text;

namespace Basic_Management.Commands.CommandExecutors
{
    public class Blob : Command
    {
        public Blob() : base("say", AccessType.USER, "Repeats the arguments with a synthesizer :D")
        {}

        public override void RunCommand(string[] args)
        {
            SpeechSynthesizer synthesizer =  new SpeechSynthesizer();
            synthesizer.SelectVoice(synthesizer.GetInstalledVoices()[0].VoiceInfo.Name);

            StringBuilder builder = new StringBuilder();
            foreach (var prompt in args)
            {
                builder.Append(prompt);
            }
            
            synthesizer.SpeakAsync(getPrompt(builder.ToString()));
        }

        private PromptBuilder getPrompt(string s)
        {
            PromptBuilder builder = new PromptBuilder();
            
            builder.StartParagraph();
            foreach (var message in s.Split('.'))
            {
                builder.StartSentence();
                builder.AppendText(message);
                builder.EndSentence();
            }
            builder.EndParagraph();

            return builder;
        }
    }
}