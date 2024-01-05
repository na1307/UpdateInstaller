namespace UpdateInstaller;

public sealed class DismWorker : UpdateWorker {
    private readonly ProcessStartInfo dismStartInfo = new() { FileName = "dism.exe", UseShellExecute = true, WindowStyle = ProcessWindowStyle.Hidden };

    public DismWorker(IEnumerable<Update> updates, Form form) : base(updates, form) { }
    public DismWorker(IEnumerable<string> updates, Form form) : base(updates, form) { }

    protected override int InstallSingle(Update update) {
        // 임시 디렉토리 생성
        DirectoryInfo sandboxDirectory = Directory.CreateDirectory(Path.Combine(Environment.GetEnvironmentVariable("temp"), update.Name));

        // Dism 매개 변수
        dismStartInfo.Arguments = $"/online /add-package /packagepath:\"{update.UpdatePath}\" /scratchdir:\"{sandboxDirectory.FullName}\" /quiet /norestart";

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
