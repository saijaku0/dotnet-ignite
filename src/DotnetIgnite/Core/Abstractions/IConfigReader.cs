using DotnetIgnite.Core.Configuration;

namespace DotnetIgnite.Core.Abstractions;

public interface IConfigReader
{
    /// <summary>
    /// Loads the Ignite configuration from the current working directory.
    /// </summary>
    /// <returns>An instance of IgniteConfig representing the loaded configuration.</returns>
    IgniteConfig LoadFromCurrentDirectory();
}
