using System.Runtime.InteropServices;

namespace UpdateInstaller;

/// <summary>
/// 업데이트 작업기
/// </summary>
public abstract class UpdateWorker : IDisposable {
    private readonly IEnumerable<Update> updates;
    private readonly IntPtr hWnd;
    private int progress;
    private bool disposedValue;

    protected UpdateWorker(IEnumerable<Update> updates, Form form) {
        this.updates = updates;
        hWnd = form.Handle;

        if (!ShutdownBlockReasonCreate(hWnd, "업데이트 설치 작업을 진행 중입니다.")) {
            throw new UpdateInstallerException("ShutdownBlockReasonCreate failed: " + Marshal.GetLastWin32Error().ToString("X"));
        }
    }

    protected UpdateWorker(IEnumerable<string> updates, Form form) : this(updates.Select(u => new Update(u)), form) { }

    ~UpdateWorker() => Dispose(disposing: false);

    /// <summary>
    /// 한 업데이트 파일의 설치를 시작할 때 발생
    /// </summary>
    public event EventHandler<UpdateInstallStartedEventArgs>? InstallStarted;

    /// <summary>
    /// 한 업데이트 파일의 설치가 끝났을 때 발생
    /// </summary>
    public event EventHandler<UpdateInstallCompletedEventArgs>? InstallCompleted;

    /// <summary>
    /// 작업을 시작함
    /// </summary>
    /// <param name="token">토큰</param>
    /// <returns></returns>
    /// <exception cref="UpdateFailedException">업데이트 설치가 실패함</exception>
    public Task WorkAsync(CancellationToken token) {
        return Task.Run(startWork, token);

        void startWork() {
            List<string> failedList = [];

            foreach (Update update in updates) {
                if (token.IsCancellationRequested) token.ThrowIfCancellationRequested();

                InstallStarted?.Invoke(this, new(update, ++progress));

                var result = InstallSingle(update);

                switch (result) {
                    case 0:
                        break;

                    case 3010:
                        Status.MustRestart = true;
                        break;

                    default:
                        failedList.Add(update.Name);
                        break;
                }

                InstallCompleted?.Invoke(this, new(progress, result));
            }

            var failedString = failedList.ToJoinedString(", ");

            if (!string.IsNullOrEmpty(failedString)) throw new UpdateInstallerException(failedString + " 업데이트 설치를 실패했습니다.");
        }
    }

    public void Dispose() {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// 업데이트 파일 하나를 설치함
    /// </summary>
    /// <param name="update">설치할 업데이트 파일의 전체 경로</param>
    /// <returns>종료 코드</returns>
    protected abstract int InstallSingle(Update update);

    protected virtual void Dispose(bool disposing) {
        if (!disposedValue) {
            if (disposing) {
                // 관리형 개체 없음
            }

            if (!ShutdownBlockReasonDestroy(hWnd)) {
                throw new UpdateInstallerException("ShutdownBlockReasonDestroy failed: " + Marshal.GetLastWin32Error().ToString("X"));
            }

            disposedValue = true;
        }
    }

    [DllImport("user32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool ShutdownBlockReasonCreate(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] string pwszReason);

    [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool ShutdownBlockReasonDestroy(IntPtr hWnd);
}
