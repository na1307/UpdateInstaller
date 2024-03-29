﻿namespace UpdateInstaller;

public sealed class DismWorker(IEnumerable<Update> updates) : UpdateWorker(updates) {
    private readonly ProcessStartInfo dismStartInfo = new() { FileName = "dism.exe", UseShellExecute = true, WindowStyle = ProcessWindowStyle.Hidden };

    protected override async Task<int> InstallSingleAsync(Update update, CancellationToken token) {
        // 임시 디렉토리 생성
        var sandboxDirectory = Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), update.Name));

        // Dism 매개 변수
        dismStartInfo.Arguments = $"/online /add-package /packagepath:\"{update.FullPath}\" /scratchdir:\"{sandboxDirectory.FullName}\" /quiet /norestart";

        using Process dism = new() { StartInfo = dismStartInfo };

        dism.Start(); // Dism 작업 시작
        await dism.WaitForExitAsync(token); // 끝날 때 까지 기다림

        try {
            sandboxDirectory.Delete(true); // 임시 디렉토리 삭제
        } catch (DirectoryNotFoundException) {
            // Do nothing
        }

        return dism.ExitCode;
    }
}
