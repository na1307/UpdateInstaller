namespace UpdateInstaller;

public static class Constants {
    public const ulong BuildNumber = 7;
    public const string PkgMgr = "PkgMgr";
    public const string Dism = "Dism";
    public const string DismApi = "Dism API";
    public const string ConfigFileName = "UpdateInstaller.xml";
    public const string OSVersion = nameof(OSVersion);
    public const string SPVersion = nameof(SPVersion);
    public const string PackageVersion = nameof(PackageVersion);
    public static readonly string Arch = IntPtr.Size == 8 ? "x64" : "x86"; // 32비트 또는 64비트 감지
}
