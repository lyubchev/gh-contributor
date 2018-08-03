using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;
using LibGit2Sharp;
using System.IO;
using System.Linq;

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

    [Verb("fill-help", HelpText = "Outputs information about starting a fill session.")]
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

    [Verb("run-fill", HelpText = "Starts new session.")]
    class RunOptions
    {
        [Option('d', "dates", Required = true, HelpText = "Date(s) to be filled.")]
        private string Dates { get; set; }

        [Option("duration", Required = false, HelpText = "The amount of dates after the original one.")]
        private int Duration { get; set; } = 0;

        [Option('p', "pattern", Required = false, HelpText = "Pattern to be used.")]
        private string Pattern { get; set; }

        [Option('c', "commits", Required = false, HelpText = "Commits for each date.")]
        private int CommitsPerDay { get; set; } = 5;

        List<DateTime> DatesList = new List<DateTime>();

        public void RunFill()
        {
            string folderName = Utility.RandomString(10);
            Directory.CreateDirectory(folderName);

            ParseAndAddDuration(Dates.Split(' '));
            Repository.Init(folderName);
            using (var repo = new Repository(folderName))
            {
                foreach (var date in DatesList)
                {
                    for (int i = 0; i < CommitsPerDay; i++)
                    {
                        string content = $"I ❤ GitHub! {date.ToString()}{Environment.NewLine}";
                        File.AppendAllText(Path.Combine(repo.Info.WorkingDirectory, "README.md"), content);
                        repo.Index.Add("README.md");
                        Commands.Stage(repo, "*");
                        Signature author = new Signature(LocalUser.Username, LocalUser.Email, date);
                        Signature committer = author;

                        Commit commit = repo.Commit("Dummit!", author, committer);
                    }
                }
            }
        }

        private void ParseAndAddDuration(string[] dates)
        {
            foreach (var date in dates)
            {
                DatesList.Add(DateTime.Parse(date));
            }

            for (int i = 0; i < Duration; i++)
            {
                DatesList.Add(DatesList[DatesList.Count - i].AddDays(i));
            }
        }
    }

    [Verb("reset", HelpText = "Resets local user settings.")]
    class ResetOptions
    {
        [Option('u', "username", Required = false, HelpText = "Resets username only")]
        private bool ResetUsername { get; set; }

        [Option('e', "email", Required = false, HelpText = "Resets email only")]
        private bool ResetEmail { get; set; }

        public void Reset()
        {
            if(!ResetUsername && !ResetEmail)
            {
                Utility.ResetUserSettings();
            }
            else if(ResetUsername && ResetEmail)
            {
                Utility.ResetUserSettings();
            }
            else if(ResetUsername)
            {
                Utility.ResetUsername();
            }
            else if(ResetEmail)
            {
                Utility.ResetEmail();
            }
        }
    }
}
