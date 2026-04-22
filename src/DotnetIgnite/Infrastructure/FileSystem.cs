using DotnetIgnite.Core.Abstractions;

namespace DotnetIgnite.Infrastructure;

public class FileSystem : IFileSystem
{
    public bool FileExists(string path) => File.Exists(path);
    public string ReadAllText(string path) => File.ReadAllText(path);
    public string GetCurrentDirectory() => Directory.GetCurrentDirectory();
    public string CombinePath(string pathA, string pathB) => Path.Combine(pathA, pathB);
}
