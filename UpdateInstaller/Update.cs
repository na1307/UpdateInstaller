namespace UpdateInstaller;

public record Update(string UpdatePath, string Description) {
    private const string unknownDesc = "(불명)";

    public string Name => Path.GetFileNameWithoutExtension(UpdatePath);

    public Update(string updatePath) : this(updatePath, unknownDesc) { }

    public virtual bool Equals(Update? other) => other is not null && UpdatePath == other.UpdatePath;

    public override int GetHashCode() => UpdatePath.GetHashCode();
}
