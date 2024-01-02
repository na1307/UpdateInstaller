using System.Runtime.InteropServices;

namespace UpdateInstaller;

public sealed class WinApiStrLogicalComparer : IComparer<string> {
    private WinApiStrLogicalComparer() { }

    public static WinApiStrLogicalComparer Shared { get; } = new();

    public static int Compare(string x, string y) => Compare(x, y, false);

    public static int Compare(string x, string y, bool reverse) {
        var value = StrCmpLogicalW(x, y);

        return !reverse ? value : value switch { < 0 => 1, 0 => 0, > 0 => -1 };

        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        static extern int StrCmpLogicalW(string x, string y);
    }

    int IComparer<string>.Compare(string x, string y) => Compare(x, y);
}
