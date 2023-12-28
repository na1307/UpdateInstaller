namespace UpdateInstaller;

/// <summary>
/// 업데이트 작업기
/// </summary>
public abstract class UpdateWorker : IDisposable {
    private readonly BackgroundWorker backgroundWorker;
    private int progress;
    private bool disposedValue;

    /// <summary>
    /// 한 업데이트 파일의 설치를 시작할 때 발생
    /// </summary>
    public event EventHandler<UpdateInfoEventArgs>? InstallStarted;

    /// <summary>
    /// 한 업데이트 파일의 설치가 끝났을 때 발생
    /// </summary>
    public event ProgressChangedEventHandler InstallCompleted {
        add => backgroundWorker.ProgressChanged += value;
        remove => backgroundWorker.ProgressChanged -= value;
    }

    /// <summary>
    /// 작업이 모두 끝났을 때 발생
    /// </summary>
    public event RunWorkerCompletedEventHandler WorkCompleted {
        add => backgroundWorker.RunWorkerCompleted += value;
        remove => backgroundWorker.RunWorkerCompleted -= value;
    }

    /// <summary>
    /// 작업이 모두 끝났는지 여부
    /// </summary>
    public bool IsCompleted => !backgroundWorker.IsBusy;

    protected UpdateWorker(IEnumerable<Update> updates) {
        backgroundWorker = new() {
            WorkerReportsProgress = true,
            WorkerSupportsCancellation = true,
        };
        backgroundWorker.DoWork += install;
        backgroundWorker.RunWorkerAsync(updates);
    }

    protected UpdateWorker(IEnumerable<string> updates) : this(updates.Select(u => new Update(u))) { }

    /// <summary>
    /// 작업을 중단함
    /// </summary>
    public void Abort() => backgroundWorker.CancelAsync();

    /// <inheritdoc/>
    public void Dispose() {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing) {
        if (!disposedValue) {
            if (disposing) {
                backgroundWorker.Dispose();
            }

            disposedValue = true;
        }
    }

    /// <summary>
    /// 업데이트 파일 하나를 설치함
    /// </summary>
    /// <param name="update">설치할 업데이트 파일의 전체 경로</param>
    /// <returns>작업의 결과</returns>
    protected abstract int InstallSingle(Update update);

    private void install(object sender, DoWorkEventArgs e) {
        Thread.Sleep(500);

        List<string> failedList = [];

        foreach (var update in (IEnumerable<Update>)e.Argument) {
            if (backgroundWorker.CancellationPending) {
                e.Cancel = true;
                break;
            }

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

            backgroundWorker.ReportProgress(progress, result);
            Thread.Sleep(200);
        }

        var failedString = failedList.ToJoinedString(", ");

        if (!string.IsNullOrEmpty(failedString)) throw new UpdateFailedException(failedString + " 업데이트 설치를 실패했습니다.");
    }

    private sealed class UpdateFailedException : UpdateInstallerException {
        public UpdateFailedException(string message) : base(message) { }
        public UpdateFailedException(string message, Exception inner) : base(message, inner) { }
    }
}
