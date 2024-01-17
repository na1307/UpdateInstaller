namespace UpdateInstaller;

public static class Constants {
    public const ulong BuildNumber = 32;
    public const string ConfigFileName = "UpdateInstaller.xml";
    public const string OSVersion = nameof(OSVersion);
    public const string SPVersion = nameof(SPVersion);
    public const string PackageVersion = nameof(PackageVersion);
    public const int WM_QUERYENDSESSION = 0x11;
    public static readonly string Arch = IntPtr.Size == 8 ? "x64" : "x86"; // 32비트 또는 64비트 감지
}
