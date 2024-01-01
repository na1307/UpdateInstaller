using UpdateInstaller.Properties;

namespace UpdateInstaller;

internal static class Program {
    [STAThread]
    private static void Main() {
        try {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 구성 파일이 없으면 예외 던지기
            if (!File.Exists(ConfigFileName)) throw new UpdateInstallerException("구성 파일을 찾을 수 없습니다.");

            // 필수 구성이 없으면 예외 던지기
            IEnumerable<string> optitems = new[] { nameof(OSVersion), nameof(SPVersion), nameof(PackageVersion) }.Where(req => GetConfigValue(req) == null);
            if (optitems.Any()) throw new UpdateInstallerException($"구성 파일에 \"{optitems.First()}\" 항목이 없습니다.");
#if !DEBUG

            // 커널이나 서비스 팩 버전이 다름
            if (GetConfigValue(OSVersion) != Environment.OSVersion.Version.ToString(2) || GetConfigValue(SPVersion) != Winver.SPLevel.ToString()) throw new UpdateInstallerException("패키지와 현재 운영 체제가 호환되지 않습니다.");
#endif

            if (GetConfigValue(OSVersion) == "6.0" && Settings.Default.PackageProgram == Dism) Settings.Default.PackageProgram = PkgMgr;

            Application.Run(MainForm.Instance);
        } catch (UpdateInstallerException e) {
            ErrMsg(e.Message);
        }
    }
}
