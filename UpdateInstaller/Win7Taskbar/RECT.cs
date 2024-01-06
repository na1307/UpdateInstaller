using System.Runtime.InteropServices;

namespace UpdateInstaller.Win7Taskbar;

[StructLayout(LayoutKind.Sequential)]
public struct RECT {
    public int Left, Top, Right, Bottom;
}
