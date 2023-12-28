namespace UpdateInstaller;

public static class UpdateWorkerFactory {
    public static UpdateWorker Create(IEnumerable<string> updates) {
        return Properties.Settings.Default.PackageProgram switch {
            PkgMgr => new PkgMgrWorker(updates),
            Dism => new DismWorker(updates),
            _ => throw new InvalidOperationException(),
        };
    }
}
