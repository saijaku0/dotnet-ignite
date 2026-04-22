using System.Text.Json;
using System.Text.Json.Serialization;
using DotnetIgnite.Core.Abstractions;
using DotnetIgnite.Core.Configuration;

namespace DotnetIgnite.Infrastructure;

public class ConfigReader(IFileSystem fileSystem) : IConfigReader
{
    private readonly IFileSystem _fileSystem = fileSystem
        ?? throw new ArgumentNullException(nameof(fileSystem));
    private const string _configFileName = ".ignite.json";
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() }
    };
    public IgniteConfig LoadFromCurrentDirectory()
    {
        string currentDirectory = _fileSystem.GetCurrentDirectory();
        string configFilePath = _fileSystem.CombinePath(currentDirectory, _configFileName);

        if (!_fileSystem.FileExists(configFilePath))
        {
            throw new FileNotFoundException(
                $"Configuration file '{_configFileName}' not found in directory '{currentDirectory}'. " +
                "Please ensure the file exists and is readable.",
                configFilePath);
        }

        string jsonContent = _fileSystem.ReadAllText(configFilePath);

        IgniteConfig config = JsonSerializer.Deserialize<IgniteConfig>(jsonContent, _jsonSerializerOptions)
            ?? throw new InvalidOperationException(
                $"Failed to deserialize '{_configFileName}'. The file content is invalid or empty.");

        if (config.Layers is null)
            throw new InvalidDataException("Missing required 'Layers' section in configuration.");

        return config;
    }
}
