namespace UpdateInstaller;

public sealed class PreUpdate {
    public required string[] Updates { get; init; }
    public required string Description { get; init; }
    public string[]? Arch { get; init; }
}
