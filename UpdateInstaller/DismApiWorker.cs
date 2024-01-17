#if NET45_OR_GREATER
using Microsoft.Dism;
#endif

namespace UpdateInstaller;

public sealed class DismApiWorker : UpdateWorker {
#if NET45_OR_GREATER
    private bool disposedValue;
#endif

    public DismApiWorker(IEnumerable<Update> updates, Form form) : base(updates, form) {
#if !NET45_OR_GREATER
        throw new NotSupportedException();
#else
        DismApi.Initialize(DismLogLevel.LogErrors);
#endif
    }

    public DismApiWorker(IEnumerable<string> updates, Form form) : base(updates, form) {
#if !NET45_OR_GREATER
        throw new NotSupportedException();
#else
        DismApi.Initialize(DismLogLevel.LogErrors);
#endif
    }

    protected override async Task<int> InstallSingleAsync(Update update, CancellationToken token) {
#if !NET45_OR_GREATER
        throw new NotSupportedException();
#else
        using DismSession session = DismApi.OpenOnlineSessionEx(new() { ThrowExceptionOnRebootRequired = false });

        try {
            await Task.Run(() => DismApi.AddPackage(session, update.FullPath, false, false), token);
        } catch (DismException dex) {
            return dex.HResult;
        }

        return session.RebootRequired ? 3010 : 0;
#endif
    }
#if NET45_OR_GREATER

    protected override void Dispose(bool disposing) {
        if (!disposedValue) {
            if (disposing) {
                // 관리형 개체 없음
            }

            DismApi.Shutdown();
            disposedValue = true;
        }

        base.Dispose(disposing);
    }
#endif
}