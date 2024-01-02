namespace UpdateInstaller;

public sealed class UpdateInstallCompletedEventArgs : EventArgs {
    public int Progress { get; }
    public int Result { get; }

    public UpdateInstallCompletedEventArgs(int progress, int result) {
        Progress = progress;
        Result = result;
    }
}