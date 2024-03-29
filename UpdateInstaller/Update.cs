﻿using System.Diagnostics.CodeAnalysis;

namespace UpdateInstaller;

public record class Update {
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

    public virtual bool Equals(Update? other) => other is not null && EqualityContract.Equals(other.EqualityContract) && FullPath == other.FullPath && Arch == other.Arch && Platform == other.Platform;
    public override int GetHashCode() => FullPath.GetHashCode() ^ Arch.GetHashCode() ^ Platform.GetHashCode();
    public sealed override string ToString() => Name;
}
