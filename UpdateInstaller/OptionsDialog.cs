using static UpdateInstaller.ConfigJsonFileHelper;
using static UpdateInstaller.Properties.Settings;
using static UpdateInstaller.WorkerType;

namespace UpdateInstaller;

public sealed partial class OptionsDialog {
    public OptionsDialog() {
        InitializeComponent();
        autoRestartBox.Checked = Default.AutoRestart;
        dismButton.Enabled = OSVersion != "6.0";
        pkgmgrButton.Checked = Default.UpdateWorker == PkgMgr;
        dismButton.Checked = Default.UpdateWorker == Dism;
    }

    protected override void OK_Button_Click(object sender, EventArgs e) {
        base.OK_Button_Click(sender, e);
        Default.AutoRestart = autoRestartBox.Checked;
        if (pkgmgrButton.Checked) Default.UpdateWorker = PkgMgr;
        if (dismButton.Checked) Default.UpdateWorker = Dism;
        Default.Save();
    }
}