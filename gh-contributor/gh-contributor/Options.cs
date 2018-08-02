using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace gh_contributor
{
    [Verb("clear", HelpText = "Clears the terminal.")]
    class ClearOptions
    {
        public void Clear()
        {
            Console.Clear();
        }
    }

    [Verb("fill help", HelpText = "Outputs information about starting a fill session.")]
    class FillHelpOptions
    {
        public void GiveInfo()
        {
            Console.WriteLine(@"    To start a new ""contribution filling"" session use ""run fill""");
            Console.WriteLine(@"    After starting a new session start typing the dates which you are willing to fill in the GitHub calendar.");
            Console.WriteLine(@"    Separate each date with comma "",""");
            Console.WriteLine(@"    Valid date formats: MM/DD/YYYY");
        }
    }

    [Verb("run fill", HelpText = "Starts new session.")]
    class RunOptions
    {
        [Option(' ', "fill help", Required = true, HelpText = "Input file to be processed.")]
        public string InputFile { get; set; }
    }

    [Verb("reset", HelpText = "Resets local user settings.")]
    class ResetOptions
    {
        [Option('u', "username", Required = true, HelpText = "Resets username only")]
        public bool ResetUsername { get; set; }

        [Option('e', "email", Required = true, HelpText = "Resets email only")]
        public bool ResetEmail { get; set; }

        public void Reset()
        {
            if(!ResetUsername && !ResetEmail)
            {
                Utility.ResetUserSettings();
            }
            else if(ResetEmail)
            {
                Utility.ResetUserSettings();
            }
        }
    }
}
