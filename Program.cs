using System.CommandLine;

namespace app;

class Program
{
    static async Task<int> Main(string[] args)
    {
        var urlOption = new Option<string?>(
            name: "--url",
            description: "The URL to take and validate."
        );

        var pathOption = new Option<string?>(
            name: "--path",
            description: "The path to take and validate."
        );

        var rootCommand = new RootCommand("A simple program that takes a URL or a path and validates it.")
        {
            urlOption,
            pathOption
        };

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
        }, urlOption, pathOption);

        return await rootCommand.InvokeAsync(args);
    }
}