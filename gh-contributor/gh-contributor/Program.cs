using CommandLine;
using System;

namespace gh_contributor
{
    class Program
    {
        static void Main(string[] args)
        {

            string userSettings = Utility.LoadUserSettings();
            if (userSettings != "")
            {
                LocalUser.Username = userSettings.Split(Environment.NewLine)[0];
                LocalUser.Email = userSettings.Split(Environment.NewLine)[1];
            }
            else
            {
                Console.WriteLine("Please configure your GitHub account settings");
                Console.Write("Username: ");
                LocalUser.Username = Console.ReadLine();
                Console.Write("Email: ");
                LocalUser.Email = Console.ReadLine();
                Console.WriteLine("Configuring settings...");
                Utility.SaveUserSettings(LocalUser.Username, LocalUser.Email);
            }
            var result = Parser.Default.ParseArguments<ClearOptions, FillHelpOptions, RunOptions, ResetOptions>(args);
            result
            .WithParsed<ClearOptions>(opts => opts.Clear())
            .WithParsed<FillHelpOptions>(opts => opts.GiveInfo())
            .WithParsed<RunOptions>(opts => opts.RunFill())
            .WithParsed<ResetOptions>(opts => opts.Reset())
            .WithNotParsed(err => Console.WriteLine("Please pass paramaters"));
        }
    }
}
