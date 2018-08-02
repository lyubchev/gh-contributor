using CommandLine;
using System;

namespace gh_contributor
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<ClearOptions, FillHelpOptions, RunOptions, ResetOptions>(args);
            result
            .WithParsed<ClearOptions>(opts => opts.Clear())
            .WithParsed<FillHelpOptions>(opts => opts.GiveInfo())
            .WithParsed<RunOptions>(opts => opts.GiveInfo())
            .WithParsed<ResetOptions>(opts => opts.GiveInfo())
            .WithNotParsed(err => Console.WriteLine(err));
        }
    }
}
