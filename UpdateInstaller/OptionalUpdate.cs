using System.Diagnostics.CodeAnalysis;

namespace UpdateInstaller;

public sealed record class OptionalUpdate : Update {
    private const string unknownDesc = "(불명)";
    private string description = unknownDesc;

    [AllowNull]
    public string Description {
        get => description;
        init => description = value is not null ? value : unknownDesc;
    }

    public bool Checked { get; set; }

    public bool Equals(OptionalUpdate? other) => base.Equals(other) && Description == other.Description;
    public override int GetHashCode() => base.GetHashCode() ^ Description.GetHashCode();
}
