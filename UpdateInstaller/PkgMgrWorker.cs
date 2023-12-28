namespace UpdateInstaller;

public sealed class PkgMgrWorker : UpdateWorker {
    private readonly ProcessStartInfo pkgMgrStartInfo = new() { FileName = "pkgmgr.exe", UseShellExecute = true, WindowStyle = ProcessWindowStyle.Hidden };

    public PkgMgrWorker(IEnumerable<string> updates) : base(updates) { }

    protected override int InstallSingle(string updateFile) {
        // 임시 디렉토리 생성
        var sandboxDirectory = Directory.CreateDirectory(Path.Combine(Environment.GetEnvironmentVariable("temp"), Path.GetFileNameWithoutExtension(updateFile)));

        // PkgMgr 매개 변수
        pkgMgrStartInfo.Arguments = $"/ip /m:\"{updateFile}\" /s:\"{sandboxDirectory.FullName}\" /quiet /norestart";

        using Process pkgMgr = new() { StartInfo = pkgMgrStartInfo };

        pkgMgr.Start(); // PkgMgr 작업 시작
        pkgMgr.WaitForExit(); // 끝날 때 까지 기다림

        try {
            sandboxDirectory.Delete(true); // 임시 디렉토리 삭제
        } catch (DirectoryNotFoundException) {
            // Do nothing
        }

        return pkgMgr.ExitCode;
    }
}
