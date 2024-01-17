namespace UpdateInstaller;

public sealed class PkgMgrWorker : UpdateWorker {
    private readonly ProcessStartInfo pkgMgrStartInfo = new() { FileName = "pkgmgr.exe", UseShellExecute = true, WindowStyle = ProcessWindowStyle.Hidden };

    public PkgMgrWorker(IEnumerable<Update> updates, Form form) : base(updates, form) { }
    public PkgMgrWorker(IEnumerable<string> updates, Form form) : base(updates, form) { }

    protected override async Task<int> InstallSingleAsync(Update update, CancellationToken token) {
        // 임시 디렉토리 생성
        DirectoryInfo sandboxDirectory = Directory.CreateDirectory(Path.Combine(Environment.GetEnvironmentVariable("temp"), update.Name));

        // PkgMgr 매개 변수
        pkgMgrStartInfo.Arguments = $"/ip /m:\"{update.FullPath}\" /s:\"{sandboxDirectory.FullName}\" /quiet /norestart";

        using Process pkgMgr = new() { StartInfo = pkgMgrStartInfo };

        pkgMgr.Start(); // PkgMgr 작업 시작
        await pkgMgr.WaitForExitAsync(token); // 끝날 때 까지 기다림

        try {
            sandboxDirectory.Delete(true); // 임시 디렉토리 삭제
        } catch (DirectoryNotFoundException) {
            // Do nothing
        }

        return pkgMgr.ExitCode;
    }
}
