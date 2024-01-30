namespace UpdateInstaller;

public sealed class UpdateInstallCompletedEventArgs(int progress, int result) : EventArgs {
    public int Progress { get; } = progress;
    public int Result { get; } = result;
}
