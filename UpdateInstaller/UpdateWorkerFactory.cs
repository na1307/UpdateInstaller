using static UpdateInstaller.WorkerType;

namespace UpdateInstaller;

public static class UpdateWorkerFactory {
    public static UpdateWorker Create(IEnumerable<string> updates, Form form) {
        return Properties.Settings.Default.UpdateWorker switch {
            PkgMgr => new PkgMgrWorker(updates, form),
            Dism => new DismWorker(updates, form),
            DismApi => new DismApiWorker(updates, form),
            _ => throw new InvalidOperationException(),
        };
    }
}
