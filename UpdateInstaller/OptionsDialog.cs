using static UpdateInstaller.Properties.Settings;
using static UpdateInstaller.WorkerType;

namespace UpdateInstaller;

public sealed partial class OptionsDialog {
    public OptionsDialog() {
        InitializeComponent();
        if (Environment.OSVersion.Version < new Version(6, 2)) dismapiButton.Enabled = false;
        autoRestartBox.Checked = Default.AutoRestart;
        dismButton.Enabled = GetConfigValue(OSVersion) != "6.0";
        pkgmgrButton.Checked = Default.UpdateWorker == PkgMgr;
        dismButton.Checked = Default.UpdateWorker == Dism;
        dismapiButton.Checked = Default.UpdateWorker == DismApi;
    }

    protected override void OK_Button_Click(object sender, EventArgs e) {
        base.OK_Button_Click(sender, e);
        Default.AutoRestart = autoRestartBox.Checked;
        if (pkgmgrButton.Checked) Default.UpdateWorker = PkgMgr;
        if (dismButton.Checked) Default.UpdateWorker = Dism;
        if (dismapiButton.Checked) Default.UpdateWorker = DismApi;
        Default.Save();
    }
}