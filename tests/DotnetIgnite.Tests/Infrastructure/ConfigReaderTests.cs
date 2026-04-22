using DotnetIgnite.Core.Abstractions;
using DotnetIgnite.Core.Configuration;
using DotnetIgnite.Core.Enums;
using DotnetIgnite.Infrastructure;
using NSubstitute;

namespace DotnetIgnite.Tests.Infrastructure;

public class ConfigReaderTests
{
    private readonly IFileSystem _fileSystem;
    private readonly ConfigReader _configReader;

    public ConfigReaderTests()
    {
        _fileSystem = Substitute.For<IFileSystem>();
        _configReader = new ConfigReader(_fileSystem);
    }

    /// <summary>
    /// Verifies that LoadFromCurrentDirectory throws a FileNotFoundException
    /// when the configuration file '.ignite.json' is not present in the current directory.
    /// </summary>
    /// <remarks>
    /// Confirms that the exception's FileName matches the expected path and that the exception
    /// message contains '.ignite.json'. Uses a mocked file system to simulate a missing file.
    /// </remarks>
    [Fact]
    public void LoadFromCurrentDirectoryFileNotFoundThrowsFileNotFoundException()
    {
        // Arrange
        string currentDir = @"C:\test";
        string configPath = Path.Combine(currentDir, ".ignite.json");

        _fileSystem.GetCurrentDirectory().Returns(currentDir);
        _fileSystem.CombinePath(currentDir, Arg.Any<string>()).Returns(configPath);
        _fileSystem.FileExists(configPath).Returns(false);

        // Act & Assert
        FileNotFoundException ex = Assert.Throws<FileNotFoundException>(() => _configReader.LoadFromCurrentDirectory());
        Assert.Equal(configPath, ex.FileName);
        Assert.Contains(".ignite.json", ex.Message);
    }

    /// <summary>
    /// Verifies that LoadFromCurrentDirectory throws an InvalidOperationException
    /// when the .ignite.json file exists but deserialization returns null.
    /// </summary>
    /// <remarks>
    /// Uses a mocked file system. ReadAllText returns content that results in a null value
    /// during deserialization, which should cause an InvalidOperationException.
    /// </remarks>
    [Fact]
    public void LoadFromCurrentDirectoryFileFoundButContentIsNullThrowsInvalidOperationException()
    {
        // Arrange
        string currentDir = @"C:\test";
        string configPath = Path.Combine(currentDir, ".ignite.json");
        const string invalidContent = "null"; // Deserialize returns null

        _fileSystem.GetCurrentDirectory().Returns(currentDir);
        _fileSystem.CombinePath(currentDir, Arg.Any<string>()).Returns(configPath);
        _fileSystem.FileExists(configPath).Returns(true);
        _fileSystem.ReadAllText(configPath).Returns(invalidContent);

        // Act & Assert
        InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => _configReader.LoadFromCurrentDirectory());
        Assert.Contains("Failed to deserialize", ex.Message);
        Assert.Contains(".ignite.json", ex.Message);
    }

    /// <summary>
    /// Verifies that a valid .ignite.json file in the current directory
    /// results in a correctly populated IgniteConfig instance.
    /// </summary>
    /// <remarks>
    /// Uses a mocked file system and validates the Architecture and layer paths
    /// (Domain, Application, Infrastructure, Api).
    /// </remarks>
    [Fact]
    public void LoadFromCurrentDirectoryValidFileReturnsIgniteConfig()
    {
        // Arrange
        string currentDir = @"C:\test";
        string configPath = Path.Combine(currentDir, ".ignite.json");
        string validJson = @"{
            ""architecture"": ""CleanArchitecture"",
            ""layers"": {
                ""domain"": ""src/Domain"",
                ""application"": ""src/Application"",
                ""infrastructure"": ""src/Infrastructure"",
                ""api"": ""src/Api""
            }
        }";

        _fileSystem.GetCurrentDirectory().Returns(currentDir);
        _fileSystem.CombinePath(currentDir, Arg.Any<string>()).Returns(configPath);
        _fileSystem.FileExists(configPath).Returns(true);
        _fileSystem.ReadAllText(configPath).Returns(validJson);

        // Act
        IgniteConfig config = _configReader.LoadFromCurrentDirectory();

        // Assert
        Assert.NotNull(config);
        Assert.Equal(ArchitectureType.CleanArchitecture, config.Architecture);
        Assert.NotNull(config.Layers);
        Assert.Equal("src/Domain", config.Layers.Domain);
        Assert.Equal("src/Application", config.Layers.Application);
        Assert.Equal("src/Infrastructure", config.Layers.Infrastructure);
        Assert.Equal("src/Api", config.Layers.Api);
    }
}
