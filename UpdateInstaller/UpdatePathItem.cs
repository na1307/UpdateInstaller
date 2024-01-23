namespace UpdateInstaller;

public sealed class UpdatePathItem {
    public required string Path { get; init; }
    public required string Description { get; init; }
    public OSPlatform Platform { get; init; } = OSPlatform.Both;
    public CpuArch Arch { get; init; } = CpuArch.All;
}
