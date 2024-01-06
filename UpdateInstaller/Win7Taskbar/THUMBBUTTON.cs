using System.Runtime.InteropServices;

namespace UpdateInstaller.Win7Taskbar;

[StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
public struct THUMBBUTTON {
    public ThumbButtonMask dwMask;
    public uint iId;
    public uint iBitmap;
    public IntPtr hIcon;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
    public string szTip;
    public ThumbButtonFlags dwFlags;
}
