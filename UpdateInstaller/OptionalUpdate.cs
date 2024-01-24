namespace UpdateInstaller;

public sealed record class OptionalUpdate : Update {
    public bool Checked { get; set; }
}
