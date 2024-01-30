namespace UpdateInstaller;

public sealed class UpdateInstallStartedEventArgs(Update update, int count) : EventArgs {
    public Update Update { get; } = update;
    public int Count { get; } = count;
}
