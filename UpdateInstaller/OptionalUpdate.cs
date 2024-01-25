using System.Diagnostics.CodeAnalysis;

namespace UpdateInstaller;

public sealed record class OptionalUpdate : Update {
    private const string unknownDesc = "(불명)";
    private string? description;

    [AllowNull]
    public string Description {
        get => description ?? unknownDesc;
        init => description = value;
    }

    public bool Checked { get; set; }
}
