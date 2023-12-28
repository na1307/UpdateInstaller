using static UpdateInstaller.Properties.Settings;

namespace UpdateInstaller;

public sealed partial class OptionsDialog {
    public OptionsDialog() {
        InitializeComponent();
        autoRestartBox.Checked = Default.AutoRestart;
        dismButton.Enabled = Package.Instance.OSVersion != "6.0";
        pkgmgrButton.Checked = Default.PackageProgram == "PkgMgr";
        dismButton.Checked = Default.PackageProgram == "Dism";
    }

    protected override void OK_Button_Click(object sender, EventArgs e) {
        base.OK_Button_Click(sender, e);
        Default.AutoRestart = autoRestartBox.Checked;
        if (pkgmgrButton.Checked) Default.PackageProgram = PkgMgr;
        if (dismButton.Checked) Default.PackageProgram = Dism;
        Default.Save();
    }
}