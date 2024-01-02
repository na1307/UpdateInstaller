namespace UpdateInstaller;

public sealed class UpdateInstallStartedEventArgs : EventArgs {
    public Update Update { get; }
    public int Count { get; }

    public UpdateInstallStartedEventArgs(Update update, int count) {
        Update = update;
        Count = count;
    }
}
