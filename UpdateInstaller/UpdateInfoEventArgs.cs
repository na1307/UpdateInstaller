namespace UpdateInstaller;

public sealed class UpdateInfoEventArgs : EventArgs {
    public string Name { get; }
    public int Count { get; }

    public UpdateInfoEventArgs(string name, int count) {
        Name = name;
        Count = count;
    }
}