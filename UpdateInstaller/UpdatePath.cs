namespace UpdateInstaller;

public sealed class UpdatePath {
    public required string Path { get; init; }
    public required string Description { get; init; }
    public string[]? Arch { get; init; }
}
