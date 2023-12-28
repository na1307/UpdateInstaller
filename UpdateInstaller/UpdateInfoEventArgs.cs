namespace UpdateInstaller;

public sealed class UpdateInfoEventArgs : EventArgs {
    public Update Update { get; }
    public int Count { get; }

    public UpdateInfoEventArgs(Update update, int count) {
        Update = update;
        Count = count;
    }
}