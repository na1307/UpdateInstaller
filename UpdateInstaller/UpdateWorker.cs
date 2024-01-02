namespace UpdateInstaller;

/// <summary>
/// 업데이트 작업기
/// </summary>
public abstract class UpdateWorker {
    private readonly IEnumerable<Update> updates;
    private int progress;

    protected UpdateWorker(IEnumerable<Update> updates) => this.updates = updates;
    protected UpdateWorker(IEnumerable<string> updates) : this(updates.Select(u => new Update(u))) { }

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

            if (!string.IsNullOrEmpty(failedString)) throw new UpdateFailedException(failedString + " 업데이트 설치를 실패했습니다.");
        }
    }

    /// <summary>
    /// 업데이트 파일 하나를 설치함
    /// </summary>
    /// <param name="update">설치할 업데이트 파일의 전체 경로</param>
    /// <returns>종료 코드</returns>
    protected abstract int InstallSingle(Update update);

    private sealed class UpdateFailedException : UpdateInstallerException {
        public UpdateFailedException(string message) : base(message) { }
        public UpdateFailedException(string message, Exception inner) : base(message, inner) { }
    }
}
