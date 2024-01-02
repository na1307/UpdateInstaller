namespace UpdateInstaller;

public static class RestartHelper {
    public static void Restart() => Process.Start(new ProcessStartInfo() { FileName = "shutdown.exe", Arguments = "/r /t 5 /f /c \"업데이트를 모두 설치했으므로 재부팅합니다.\"", WindowStyle = ProcessWindowStyle.Hidden });
}
