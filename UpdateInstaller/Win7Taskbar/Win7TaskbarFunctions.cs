using System.Runtime.InteropServices;

namespace UpdateInstaller.Win7Taskbar;

public static class Win7TaskbarFunctions {
    private static readonly ITaskbarList4 taskbarList = (ITaskbarList4)new TaskbarList();
    private static readonly bool taskbarSupported = Environment.OSVersion.Version >= new Version(6, 1);

    public static void SetProgressState(IntPtr windowHandle, TaskbarStates taskbarState) {
        if (taskbarSupported) {
            taskbarList.SetProgressState(windowHandle, taskbarState);
        }
    }

    public static void SetProgressValue(IntPtr windowHandle, ulong progressValue, ulong progressMax) {
        if (taskbarSupported) {
            taskbarList.SetProgressValue(windowHandle, progressValue, progressMax);
        }
    }

    [ComImport]
    [Guid("c43dc798-95d1-4bea-9030-bb99e2983a1a")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface ITaskbarList4 {
        // ITaskbarList
        [PreserveSig]
        void HrInit();
        [PreserveSig]
        void AddTab(IntPtr hwnd);
        [PreserveSig]
        void DeleteTab(IntPtr hwnd);
        [PreserveSig]
        void ActivateTab(IntPtr hwnd);
        [PreserveSig]
        void SetActiveAlt(IntPtr hwnd);

        // ITaskbarList2
        [PreserveSig]
        void MarkFullscreenWindow(IntPtr hwnd, [MarshalAs(UnmanagedType.Bool)] bool fFullscreen);

        // ITaskbarList3
        [PreserveSig]
        void SetProgressValue(IntPtr hwnd, ulong ullCompleted, ulong ullTotal);
        [PreserveSig]
        void SetProgressState(IntPtr hwnd, TaskbarStates tbpFlags);
        [PreserveSig]
        void RegisterTab(IntPtr hwndTab, IntPtr hwndMDI);
        [PreserveSig]
        void UnregisterTab(IntPtr hwndTab);
        [PreserveSig]
        void SetTabOrder(IntPtr hwndTab, IntPtr hwndInsertBefore);
        [PreserveSig]
        void SetTabActive(IntPtr hwndTab, IntPtr hwndMDI, uint dwReserved = 0);
        [PreserveSig]
        void ThumbBarAddButtons(IntPtr hwnd, uint cButtons, [MarshalAs(UnmanagedType.LPArray)] THUMBBUTTON[] pButtons);
        [PreserveSig]
        void ThumbBarUpdateButtons(IntPtr hwnd, uint cButtons, [MarshalAs(UnmanagedType.LPArray)] THUMBBUTTON[] pButtons);
        [PreserveSig]
        void ThumbBarSetImageList(IntPtr hwnd, IntPtr himl);
        [PreserveSig]
        void SetOverlayIcon(IntPtr hwnd, IntPtr hIcon, [MarshalAs(UnmanagedType.LPWStr)] string pszDescription);
        [PreserveSig]
        void SetThumbnailTooltip(IntPtr hwnd, [MarshalAs(UnmanagedType.LPWStr)] string pszTip);
        [PreserveSig]
        void SetThumbnailClip(IntPtr hwnd, in RECT prcClip);

        // ITaskbarList4
        [PreserveSig]
        void SetTabProperties(IntPtr hwndTab, StpFlag stpFlags);
    }

    [ComImport]
    [Guid("56fdf344-fd6d-11d0-958a-006097c9a090")]
    [ClassInterface(ClassInterfaceType.None)]
    private class TaskbarList { }
}
