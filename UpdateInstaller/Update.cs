using System.Diagnostics.CodeAnalysis;

namespace UpdateInstaller;

public sealed record class Update {
    private const string unknownDesc = "(불명)";
    private readonly string? name;
    private readonly string fullPath = string.Empty;

    public Update() { }

    [SetsRequiredMembers]
    public Update(string fullPath) => FullPath = fullPath;

    [AllowNull]
    public string Name {
        get => name ?? Path.GetFileNameWithoutExtension(FullPath);
        init => name = value;
    }

    public string Description { get; init; } = unknownDesc;

    public required string FullPath {
        get => fullPath;
        init {
            if (!File.Exists(value)) {
                throw new FileNotFoundException("업데이트 파일이 존재하지 않습니다.", value);
            }

            fullPath = value;
        }
    }

    public CpuArch Arch { get; init; } = CpuArch.All;
    public OSPlatform Platform { get; init; } = OSPlatform.Both;

    public bool Equals(Update? other) => other is not null && FullPath == other.FullPath;
    public override int GetHashCode() => FullPath.GetHashCode();
    public override string ToString() => Name;
}
