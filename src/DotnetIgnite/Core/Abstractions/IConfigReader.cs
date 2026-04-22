using DotnetIgnite.Core.Configuration;

namespace DotnetIgnite.Core.Abstractions;

public interface IConfigReader
{
    IgniteConfig LoadFromCurrentDirectory();
}
