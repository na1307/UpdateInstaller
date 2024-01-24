namespace UpdateInstaller;

public static class Constants {
    public const ulong BuildNumber = 82;
    public const string ConfigFileName = "UpdateInstaller.json";
    public const double Vista = 6.0;
    public const double Seven = 6.1;
    public const double Eight = 6.2;
    public const double Blue = 6.3;
    public const int WM_QUERYENDSESSION = 0x11;
    public static readonly CpuArch Arch = IntPtr.Size == 8 ? CpuArch.x64 : CpuArch.x86; // 32비트 또는 64비트 감지
    public static readonly OSPlatform Platform = !Winver.IsWindowsServer ? OSPlatform.Client : OSPlatform.Server;
}
