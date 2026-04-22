using DotnetIgnite.Core.Abstractions;
using DotnetIgnite.Infrastructure;
using NSubstitute;

namespace DotnetIgnite.Tests.Infrastructure;

public class ConfigReaderTests
{
    [Fact]
    public void LoadFromCurrentDirectoryFileNotFoundThrowsFileNotFoundException()
    {
        // Arrange
        IFileSystem fileSystem = Substitute.For<IFileSystem>();
        string currentDirectory = @"C:\test";
        string expectedConfigPath = Path.Combine(currentDirectory, ".ignite.json");

        fileSystem.GetCurrentDirectory().Returns(currentDirectory);
        fileSystem.CombinePath(currentDirectory, Arg.Any<string>())
                  .Returns(expectedConfigPath);
        fileSystem.FileExists(expectedConfigPath).Returns(false);

        var configReader = new ConfigReader(fileSystem);

        // Act & Assert
        FileNotFoundException exception = Assert.Throws<FileNotFoundException>(() => configReader.LoadFromCurrentDirectory());

        // Check folder path and file name are included in the exception message
        Assert.Contains(".ignite.json", exception.Message);
        Assert.Contains(currentDirectory, exception.Message);
        Assert.Equal(expectedConfigPath, exception.FileName);
    }
}
