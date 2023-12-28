namespace UpdateInstaller;

public sealed class DismWorker : UpdateWorker {
    private readonly ProcessStartInfo dismStartInfo = new() { FileName = "dism.exe", UseShellExecute = true, WindowStyle = ProcessWindowStyle.Hidden };

    public DismWorker(IEnumerable<string> updates) : base(updates) { }

    protected override int InstallSingle(string updateFile) {
        // 임시 디렉토리 생성
        var sandboxDirectory = Directory.CreateDirectory(Path.Combine(Environment.GetEnvironmentVariable("temp"), Path.GetFileNameWithoutExtension(updateFile)));

        // Dism 매개 변수
        dismStartInfo.Arguments = $"/online /add-package /packagepath:\"{updateFile}\" /scratchdir:\"{sandboxDirectory.FullName}\" /quiet /norestart";

        using Process dism = new() { StartInfo = dismStartInfo };

        dism.Start(); // Dism 작업 시작
        dism.WaitForExit(); // 끝날 때 까지 기다림

        try {
            sandboxDirectory.Delete(true); // 임시 디렉토리 삭제
        } catch (DirectoryNotFoundException) {
            // Do nothing
        }

        return dism.ExitCode;
    }
}
