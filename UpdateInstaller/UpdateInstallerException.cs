namespace UpdateInstaller;

public class UpdateInstallerException : Exception {
    public UpdateInstallerException(string message) : base(message) { }
    public UpdateInstallerException(string message, Exception inner) : base(message, inner) { }
}
