using System.CommandLine;

namespace app;

class Program
{
    static async Task<int> Main(string[] args)
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

        pathArg.AddValidator((path) => {

            if (true){
                path.ErrorMessage = $"Invalid URL: {url.Tokens[0].Value}";
            }
        });

        pathCommand.AddArgument(pathArg);

        rootCommand.AddCommand(urlCommand);
        rootCommand.AddCommand(pathCommand);

        urlCommand.SetHandler((url) => {
            Console.WriteLine($"You entered URL: {url}");
                Console.WriteLine(URLValidator.IsValidURL(url));
        }, urlArg);

        

        pathCommand.SetHandler((path)=> {
            Console.WriteLine($"You entered path: {path}");
        }, pathArg);
        /*
        rootCommand.SetHandler((string? url, string? path) =>
        {
            if (url != null)
            {
                Console.WriteLine($"You entered URL: {url}");
                Console.WriteLine(URLValidator.IsValidURL(url));
                
            }

            if (path != null)
            {
                Console.WriteLine($"You entered path: {path}");
            }

            if (url == null && path == null)
            {
                Console.WriteLine("Please provide either a URL or a path.");
            }
        }, urlOption, pathOption);*/

        return await rootCommand.InvokeAsync(args);
    }
}