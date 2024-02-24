using System.Runtime.InteropServices;

namespace UpdateInstaller;

/// <summary>
/// 업데이트 작업기
/// </summary>
public abstract class UpdateWorker(IEnumerable<Update> updates) : IDisposable {
    private int progress;

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
    /// <exception cref="UpdateInstallerException">업데이트 설치가 실패함</exception>
    public async Task WorkAsync(CancellationToken token) {
        List<string> failedList = [];

        foreach (var update in updates) {
            token.ThrowIfCancellationRequested();
            progress++;
            InstallStarted?.Invoke(this, new UpdateInstallStartedEventArgs(update, progress));

            var result = await InstallSingleAsync(update, token);

            InstallCompleted?.Invoke(this, new UpdateInstallCompletedEventArgs(progress, result));

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
        }

        var failedString = failedList.ToJoinedString(", ");

        if (!string.IsNullOrEmpty(failedString)) {
            throw new UpdateInstallerException(failedString + " 업데이트 설치를 실패했습니다.");
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
    /// <param name="token">토큰</param>
    /// <returns>종료 코드</returns>
    protected abstract Task<int> InstallSingleAsync(Update update, CancellationToken token);

    protected virtual void Dispose(bool disposing) { }

    [DllImport("user32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool ShutdownBlockReasonCreate(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] string pwszReason);

    [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool ShutdownBlockReasonDestroy(IntPtr hWnd);
}
