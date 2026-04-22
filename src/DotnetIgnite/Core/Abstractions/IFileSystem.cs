namespace DotnetIgnite.Core.Abstractions;

public interface IFileSystem
{
    bool FileExists(string path);
    string ReadAllText(string path);
    string GetCurrentDirectory();
    string CombinePath(string pathA, string pathB);
}
