namespace UpdateInstaller;

public sealed class PreUpdateItem {
    public required Update[] Updates { get; init; }
    public required string Description { get; init; }
}
