namespace UpdateInstaller;

public sealed class UpdateInstallStartedEventArgs : EventArgs {
    public UpdateInstallStartedEventArgs(Update update, int count) {
        Update = update;
        Count = count;
    }

    public Update Update { get; }
    public int Count { get; }
}
