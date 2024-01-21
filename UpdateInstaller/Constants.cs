namespace UpdateInstaller;

public static class Constants {
    public const ulong BuildNumber = 64;
    public const string ConfigFileName = "UpdateInstaller.json";
    public const int WM_QUERYENDSESSION = 0x11;
    public static readonly CpuArch Arch = (CpuArch)Enum.Parse(typeof(CpuArch), IntPtr.Size == 8 ? "x64" : "x86"); // 32비트 또는 64비트 감지
}
