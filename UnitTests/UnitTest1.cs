using RepoLinter;

namespace UnitTests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var git = new Git("https://github.com/EmilZackrisson/RepositoryLinter.git");
        var exists = Directory.Exists(git.PathToGitRepository);
        Assert.True(exists);
    }
}