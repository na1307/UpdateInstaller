namespace UpdateInstaller;

public sealed class UpdateInstallCompletedEventArgs : EventArgs {
    public UpdateInstallCompletedEventArgs(int progress, int result) {
        Progress = progress;
        Result = result;
    }

    public int Progress { get; }
    public int Result { get; }
}
