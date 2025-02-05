using System.CommandLine;
using System.IO;

namespace app;

class Program
{
    private static async Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand("A simple program that takes a URL or a path and validates it.");

        var urlCommand = new Command("url", "Run linter on URL");
        var urlArg = new Argument<string>("url", "URL");

        urlArg.AddValidator((url) => {

            if (!URLValidator.IsValidURL(url.Tokens[0].Value)){
                url.ErrorMessage = $"Invalid URL: {url.Tokens[0].Value}";
            }
        });

        urlCommand.AddArgument(urlArg);

        var pathCommand = new Command("path", "Run linter on path");
        var pathArg = new Argument<string>("path", "Path");

        pathArg.AddValidator((path) =>
        {
            if (!pathValidator.isValidPath(path.Tokens[0].Value)){
                path.ErrorMessage = $"Invalid Path: {path.Tokens[0].Value}";
            }
        });

        pathCommand.AddArgument(pathArg);

        rootCommand.AddCommand(urlCommand);
        rootCommand.AddCommand(pathCommand);

        urlCommand.SetHandler((url) => {
            Console.WriteLine($"You entered URL: {url}");
            var git = new Git(url);
            git.Clone();
        }, urlArg);

        

        pathCommand.SetHandler((path)=> {
            Console.WriteLine($"You entered path: {path}");
        }, pathArg);

        return await rootCommand.InvokeAsync(args);
    }
}