namespace DotnetIgnite.Core.Abstractions;

public interface IFileSystem
{
    /// <summary>
    /// Determines whether the specified file exists.
    /// </summary>
    /// <param name="path">The path to the file.</param>
    /// <returns>true if the file exists; otherwise, false.</returns>
    bool FileExists(string path);
    /// <summary>
    /// Opens a file, reads all text, and then closes the file.
    /// </summary>
    /// <param name="path">The file to open for reading. Can be a relative or absolute path.</param>
    /// <returns>A string containing all text in the file.</returns>
    string ReadAllText(string path);
    /// <summary>
    /// Gets the current working directory of the application.
    /// </summary>
    /// <returns>The path of the current working directory.</returns>
    string GetCurrentDirectory();
    /// <summary>
    /// Combines two strings into a path.
    /// </summary>
    /// <param name="pathA">The first path to combine.</param>
    /// <param name="pathB">The second path to combine.</param>
    /// <returns>
    /// The combined paths. If one of the specified paths is a zero-length string,
    /// this method returns the other path. If <paramref name="pathB"/> contains an absolute path,
    /// this method returns <paramref name="pathB"/>.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    /// <paramref name="pathA"/> or <paramref name="pathB"/> is null.
    /// </exception>
    /// <exception cref="System.ArgumentException">
    /// <paramref name="pathA"/> or <paramref name="pathB"/> contains invalid characters.
    /// </exception>
    string CombinePath(string pathA, string pathB);
}
