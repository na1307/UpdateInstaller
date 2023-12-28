using System.Runtime.InteropServices;

namespace UpdateInstaller;

public sealed class WinApiStrLogicalComparer : IComparer<string> {
    public static WinApiStrLogicalComparer Shared { get; } = new();

    private WinApiStrLogicalComparer() { }

    public static int Compare(string x, string y) => Compare(x, y, false);

    public static int Compare(string x, string y, bool reverse) {
        var var = StrCmpLogicalW(x, y);

        return !reverse ? var : var switch { < 0 => 1, 0 => 0, > 0 => -1 };

        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        static extern int StrCmpLogicalW(string x, string y);
    }

    int IComparer<string>.Compare(string x, string y) => Compare(x, y);
}