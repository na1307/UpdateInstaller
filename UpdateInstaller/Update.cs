namespace UpdateInstaller;

public sealed record class Update {
    private const string unknownDesc = "(불명)";

    public Update(string fullPath) : this(fullPath, unknownDesc) { }

    public Update(string fullPath, string description) {
        if (!File.Exists(fullPath)) {
            throw new FileNotFoundException("업데이트 파일이 존재하지 않습니다.", fullPath);
        }

        FullPath = fullPath;
        Description = description;
    }

    public string Name => Path.GetFileNameWithoutExtension(FullPath);
    public string Description { get; }
    public string FullPath { get; }

    public bool Equals(Update? other) => other is not null && FullPath == other.FullPath;
    public override int GetHashCode() => FullPath.GetHashCode();
}
