namespace UpdateInstaller;

public static class Constants {
    public const ulong BuildNumber = 35;
    public const string ConfigFileName = "UpdateInstaller.json";
    public const int WM_QUERYENDSESSION = 0x11;
    public static readonly string Arch = IntPtr.Size == 8 ? "x64" : "x86"; // 32비트 또는 64비트 감지
}
