using DotnetIgnite.Core.Enums;

namespace DotnetIgnite.Core.Configuration;

public class IgniteConfig
{
    public ArchitectureType Architecture
    {
        get;
        set
        {
            if (!Enum.IsDefined(value))
                throw new ArgumentOutOfRangeException(nameof(value), value, $"Invalid architecture type.");
            field = value;
        }

    }

    private LayersConfig? _layers;
    public required LayersConfig Layers
    {
        get => _layers!;
        set => _layers = value ?? throw new ArgumentNullException(nameof(value));
    }
}
