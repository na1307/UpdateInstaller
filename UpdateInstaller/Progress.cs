using Bluehill.TaskbarMethods;
using System.Runtime.CompilerServices;

namespace UpdateInstaller;

public sealed partial class Progress {
    private const string progressText = "업데이트 {0}개 중 {1}개 설치 완료 ({2}%)";
    private const short aggressiveInlining = 256;
    private static readonly bool isWin7orGreater = Environment.OSVersion.Version >= new Version(6, 1);
    private static bool systemShutdown;
    private readonly UpdateWorker linkedWorker;
#pragma warning disable CS0618 // 형식 또는 멤버는 사용되지 않습니다.
    private readonly CancellationTokenSource cancellation;
#pragma warning restore CS0618 // 형식 또는 멤버는 사용되지 않습니다.
    private Task? workerTask;

    public Progress(IEnumerable<Update> updates) {
        InitializeComponent();
        Icon = Application.OpenForms[0].Icon;
        progressBar1.Maximum = updates.Count();
        okButton.Enabled = false;
        label1.Text = string.Format(progressText, progressBar1.Maximum, 0, 0);
        cancellation = new();
        linkedWorker = UpdateWorkerFactory.Create(updates, this);
        linkedWorker.InstallStarted += linkedWorker_InstallStarted;
        linkedWorker.InstallCompleted += linkedWorker_InstallCompleted;
    }

    protected override async void OnShown(EventArgs e) {
        base.OnShown(e);

        setProgressState(Handle, TaskbarStates.Normal);
        workerTask = linkedWorker.WorkAsync(cancellation.Token);

        try {
            await workerTask;
        } catch {
            // Do Nothing
        }

        if (!systemShutdown) {
            if (Status.MustRestart && Properties.Settings.Default.AutoRestart) {
                RestartHelper.Restart();
                Application.Exit();
                return;
            } else if (workerTask.IsFaulted) {
                setProgressState(Handle, TaskbarStates.Error);
                textBox1.AppendText("작업 중 오류가 발생했습니다.");
                ErrMsg(workerTask.Exception.InnerException.Message);
            } else if (workerTask.IsCanceled) {
                setProgressState(Handle, TaskbarStates.Error);
                textBox1.AppendText("작업을 취소했습니다.");
                ErrMsg("작업을 취소했습니다.");
            } else {
                setProgressState(Handle, TaskbarStates.NoProgress);
                textBox1.AppendText("작업을 완료했습니다.");
                MessageBox.Show("작업을 완료했습니다." + (Status.MustRestart ? "\r\n\r\n지금 다시 시작하는 것이 좋습니다." : string.Empty), "작업 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        okButton.Enabled = true;
        cancelButton.Enabled = false;
    }

    protected override void OnFormClosing(FormClosingEventArgs e) {
        base.OnFormClosing(e);

        if (workerTask is not null && !workerTask.IsCompleted && !systemShutdown) {
            e.Cancel = true;

            if (MessageBox.Show("정말 작업을 취소할까요?", "취소", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes) {
                cancellation.Cancel();
                cancelButton.Enabled = false;
                textBox1.AppendText("\r\n취소하는 중입니다. 잠시 기다려 주세요...");
            }
        } else if (systemShutdown) {
            cancellation.Cancel();
        }
    }

    protected override void OnFormClosed(FormClosedEventArgs e) {
        base.OnFormClosed(e);
        MainForm.Instance.Show();
    }

    protected override void WndProc(ref Message m) {
        if (m.Msg == WM_QUERYENDSESSION) {
            systemShutdown = true;
        }

        base.WndProc(ref m);
    }

    [MethodImpl(aggressiveInlining)]
    private static void setProgressState(IntPtr windowHandle, TaskbarStates taskbarState) {
        if (isWin7orGreater) {
            Win7TaskbarMethods.SetProgressState(windowHandle, taskbarState);
        }
    }

    [MethodImpl(aggressiveInlining)]
    private static void setProgressValue(IntPtr windowHandle, int progressValue, int progressMax) {
        if (isWin7orGreater) {
            Win7TaskbarMethods.SetProgressValue(windowHandle, progressValue, progressMax);
        }
    }

    private void okButton_Click(object sender, EventArgs e) => Close();
    private void cancelButton_Click(object sender, EventArgs e) => Close();
    private void linkedWorker_InstallStarted(object sender, UpdateInstallStartedEventArgs e) => textBox1.AppendText($"{e.Update.Name} 설치 중(업데이트 {e.Count} / {progressBar1.Maximum})...");

    private void linkedWorker_InstallCompleted(object sender, UpdateInstallCompletedEventArgs e) {
        setProgressValue(Handle, e.Progress, progressBar1.Maximum);
        textBox1.AppendText(e.Result switch {
            0 or 3010 => " 성공.\r\n",
            _ => " 실패. (코드: " + e.Result + ")\r\n",
        });

        progressBar1.Value = e.Progress;
        label1.Text = string.Format(progressText, progressBar1.Maximum, e.Progress, Math.Truncate((double)e.Progress / progressBar1.Maximum * 100));
    }
}
