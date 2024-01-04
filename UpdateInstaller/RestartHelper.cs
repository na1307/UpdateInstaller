using System.Runtime.InteropServices;

namespace UpdateInstaller;

public static class RestartHelper {
    private const uint TOKEN_QUERY = 0x0008;
    private const uint TOKEN_ADJUST_PRIVILEGES = 0x0020;
    private const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";
    private const uint SE_PRIVILEGE_ENABLED = 0x0002;
    private const string shutdownMessage = "업데이트 작업을 완료했습니다. 시스템을 다시 시작합니다.";

    public static void Restart() {
        if (!OpenProcessToken(Process.GetCurrentProcess().Handle, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, out var th)) {
            throw new Exception("opt failed " + Marshal.GetLastWin32Error().ToString("X"));
        }

        if (!LookupPrivilegeValueW(null, SE_SHUTDOWN_NAME, out var test)) {
            throw new Exception("lpv failed " + Marshal.GetLastWin32Error().ToString("X"));
        }

        TOKEN_PRIVILEGES tp = new() { PrivilegeCount = 1, Privileges = [new() { Attributes = SE_PRIVILEGE_ENABLED, Luid = test }] };

        if (!AdjustTokenPrivileges(th, false, in tp, 100, out _, out _)) {
            throw new Exception("atp failed " + Marshal.GetLastWin32Error().ToString("X"));
        }

        if (!InitiateSystemShutdownExW(null, shutdownMessage, 30, false, true, ShutdownReason.SHTDN_REASON_MAJOR_OTHER | ShutdownReason.SHTDN_REASON_MINOR_OTHER | ShutdownReason.SHTDN_REASON_FLAG_PLANNED)) {
            throw new Exception("issex failed " + Marshal.GetLastWin32Error().ToString("X"));
        }
    }

    [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool OpenProcessToken(IntPtr ProcessHandle, uint DesiredAccess, out IntPtr TokenHandle);

    [DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool LookupPrivilegeValueW([MarshalAs(UnmanagedType.LPWStr)] string? lpSystemName, [MarshalAs(UnmanagedType.LPWStr)] string lpName, out LUID lpLuid);

    [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool AdjustTokenPrivileges(IntPtr TokenHandle, [MarshalAs(UnmanagedType.Bool)] bool DisableAllPrivileges, in TOKEN_PRIVILEGES NewState, uint BufferLength, out TOKEN_PRIVILEGES PreviousState, out uint ReturnLength);

    [DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool InitiateSystemShutdownExW(
        [MarshalAs(UnmanagedType.LPWStr)] string? lpMachineName,
        [MarshalAs(UnmanagedType.LPWStr)] string? lpMessage,
        uint dwTimeout,
        [MarshalAs(UnmanagedType.Bool)] bool bForceAppsClosed,
        [MarshalAs(UnmanagedType.Bool)] bool bRebootAfterShutdown,
        ShutdownReason dwReason);

    [Flags]
    private enum ShutdownReason : uint {
        // Major reasons
        SHTDN_REASON_MAJOR_OTHER = 0x00000000,
        SHTDN_REASON_MAJOR_NONE = SHTDN_REASON_MAJOR_OTHER,
        SHTDN_REASON_MAJOR_HARDWARE = 0x00010000,
        SHTDN_REASON_MAJOR_OPERATINGSYSTEM = 0x00020000,
        SHTDN_REASON_MAJOR_SOFTWARE = 0x00030000,
        SHTDN_REASON_MAJOR_APPLICATION = 0x00040000,
        SHTDN_REASON_MAJOR_SYSTEM = 0x00050000,
        SHTDN_REASON_MAJOR_POWER = 0x00060000,
        SHTDN_REASON_MAJOR_LEGACY_API = 0x00070000,

        // Minor reasons
        SHTDN_REASON_MINOR_OTHER = SHTDN_REASON_MAJOR_OTHER,
        SHTDN_REASON_MINOR_NONE = 0x000000ff,
        SHTDN_REASON_MINOR_MAINTENANCE = 0x00000001,
        SHTDN_REASON_MINOR_INSTALLATION = 0x00000002,
        SHTDN_REASON_MINOR_UPGRADE = 0x00000003,
        SHTDN_REASON_MINOR_RECONFIG = 0x00000004,
        SHTDN_REASON_MINOR_HUNG = 0x00000005,
        SHTDN_REASON_MINOR_UNSTABLE = 0x00000006,
        SHTDN_REASON_MINOR_DISK = 0x00000007,
        SHTDN_REASON_MINOR_PROCESSOR = 0x00000008,
        SHTDN_REASON_MINOR_NETWORKCARD = 0x00000009,
        SHTDN_REASON_MINOR_POWER_SUPPLY = 0x0000000a,
        SHTDN_REASON_MINOR_CORDUNPLUGGED = 0x0000000b,
        SHTDN_REASON_MINOR_ENVIRONMENT = 0x0000000c,
        SHTDN_REASON_MINOR_HARDWARE_DRIVER = 0x0000000d,
        SHTDN_REASON_MINOR_OTHERDRIVER = 0x0000000e,
        SHTDN_REASON_MINOR_BLUESCREEN = 0x0000000F,
        SHTDN_REASON_MINOR_SERVICEPACK = 0x00000010,
        SHTDN_REASON_MINOR_HOTFIX = 0x00000011,
        SHTDN_REASON_MINOR_SECURITYFIX = 0x00000012,
        SHTDN_REASON_MINOR_SECURITY = 0x00000013,
        SHTDN_REASON_MINOR_NETWORK_CONNECTIVITY = 0x00000014,
        SHTDN_REASON_MINOR_WMI = 0x00000015,
        SHTDN_REASON_MINOR_SERVICEPACK_UNINSTALL = 0x00000016,
        SHTDN_REASON_MINOR_HOTFIX_UNINSTALL = 0x00000017,
        SHTDN_REASON_MINOR_SECURITYFIX_UNINSTALL = 0x00000018,
        SHTDN_REASON_MINOR_MMC = 0x00000019,
        SHTDN_REASON_MINOR_SYSTEMRESTORE = 0x0000001a,
        SHTDN_REASON_MINOR_TERMSRV = 0x00000020,
        SHTDN_REASON_MINOR_DC_PROMOTION = 0x00000021,
        SHTDN_REASON_MINOR_DC_DEMOTION = 0x00000022,

        // Other
        SHTDN_REASON_FLAG_USER_DEFINED = 0x40000000,
        SHTDN_REASON_FLAG_PLANNED = 0x80000000,
    }

    private struct TOKEN_PRIVILEGES {
        public uint PrivilegeCount;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public LUID_AND_ATTRIBUTES[] Privileges;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct LUID_AND_ATTRIBUTES {
        public LUID Luid;
        public uint Attributes;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct LUID {
        public uint LowPart;
        public int HighPart;
    }
}
