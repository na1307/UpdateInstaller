namespace UpdateInstaller;

public static class Constants {
    public const ulong BuildNumber = 66;
    public const string ConfigFileName = "UpdateInstaller.json";
    public const int WM_QUERYENDSESSION = 0x11;
    public static readonly CpuArch Arch = IntPtr.Size == 8 ? CpuArch.x64 : CpuArch.x86; // 32비트 또는 64비트 감지
    public static readonly OSPlatform Platform = !Winver.IsWindowsServer ? OSPlatform.Client : OSPlatform.Server;
}
