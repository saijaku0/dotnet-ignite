namespace DotnetIgnite.Core.Configuration;

public class LayersConfig
{
    private string _domain = null!;
    private string _application = null!;
    private string _infrastructure = null!;
    private string _api = null!;

    public required string Domain
    {
        get => _domain;
        set => _domain = value ?? throw new ArgumentNullException(nameof(value));
    }
    public required string Application
    {
        get => _application;
        set => _application = value ?? throw new ArgumentNullException(nameof(value));
    }
    public required string Infrastructure
    {
        get => _infrastructure;
        set => _infrastructure = value ?? throw new ArgumentNullException(nameof(value));
    }
    public required string Api
    {
        get => _api;
        set => _api = value ?? throw new ArgumentNullException(nameof(value));
    }
}
