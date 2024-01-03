using static UpdateInstaller.WorkerType;

namespace UpdateInstaller;

public static class UpdateWorkerFactory {
    public static UpdateWorker Create(IEnumerable<string> updates) {
        return Properties.Settings.Default.UpdateWorker switch {
            PkgMgr => new PkgMgrWorker(updates),
            Dism => new DismWorker(updates),
            _ => throw new InvalidOperationException(),
        };
    }
}
