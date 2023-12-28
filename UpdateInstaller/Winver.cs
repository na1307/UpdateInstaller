using System.Runtime.InteropServices;
using static System.Environment;

namespace UpdateInstaller;

public static class Winver {
    private static readonly bool isWindowsServer = IsOS(29);

    public static int SPLevel => string.IsNullOrEmpty(OSVersion.ServicePack) ? 0 : int.Parse(OSVersion.ServicePack.Split().Last());

    public static bool IsWindowsServer => isWindowsServer;

    [DllImport("shlwapi.dll", EntryPoint = "#437", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsOS(uint os);
}
