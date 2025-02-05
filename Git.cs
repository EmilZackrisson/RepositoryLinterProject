using System.Diagnostics;

class Git
{
    private readonly string _url;
    public readonly string PathToGitRepository;

    public Git(string url, string pathToGitRepository = "")
    {
        _url = url;
        PathToGitRepository = pathToGitRepository == "" ? Path.Join(Directory.GetCurrentDirectory(), "git") : pathToGitRepository;
    }
    public void Clone()
    {
        Console.WriteLine(PathToGitRepository);
        var p = new Process
        {
            StartInfo =
            {
                FileName = "git",
                Arguments = $"clone {_url} git",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            }
        };
        var started = p.Start();
        if (!started)
        {
            throw new Exception("Failed to clone git repository");
        }
        p.WaitForExit();
    }
}