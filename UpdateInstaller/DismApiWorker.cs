#if NET45_OR_GREATER
using Microsoft.Dism;
#endif

namespace UpdateInstaller;

public sealed class DismApiWorker : UpdateWorker {
#if NET45_OR_GREATER
    private bool disposedValue;

#endif
    public DismApiWorker(IEnumerable<Update> updates) : base(updates) {
#if !NET45_OR_GREATER
        throw new NotSupportedException();
#else
        DismApi.Initialize(DismLogLevel.LogErrors);
#endif
    }

    protected override
#if NET45_OR_GREATER
        async
#endif
        Task<int> InstallSingleAsync(Update update, CancellationToken token) {
#if !NET45_OR_GREATER
        throw new NotSupportedException();
#else
        using var session = await Task.Run(() => DismApi.OpenOnlineSessionEx(new DismSessionOptions { ThrowExceptionOnRebootRequired = false }));

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
