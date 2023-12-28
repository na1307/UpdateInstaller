using UpdateInstaller.Properties;

namespace UpdateInstaller;

internal static class Program {
    [STAThread]
    private static void Main() {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        try {
            _ = Package.Instance;
        } catch (Exception ex) {
            ErrMsg(ex.InnerException.Message);
            return;
        }

        if (Package.Instance.OSVersion == "6.0" && Settings.Default.PackageProgram == Dism) Settings.Default.PackageProgram = PkgMgr;

        Application.Run(MainForm.Instance);
    }
}
