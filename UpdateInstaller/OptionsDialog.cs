using static UpdateInstaller.ConfigJsonFileHelper;
using static UpdateInstaller.Properties.Settings;
using static UpdateInstaller.WorkerType;

namespace UpdateInstaller;

public sealed partial class OptionsDialog {
    public OptionsDialog() {
        InitializeComponent();
        autoRestartBox.Checked = Default.AutoRestart;
        dismButton.Enabled = OSVersion > Vista;

        if (OSVersion < Eight) {
            dismapiButton.Enabled = false;
        }

        pkgmgrButton.Checked = Default.UpdateWorker == PkgMgr;
        dismButton.Checked = Default.UpdateWorker == Dism;
        dismapiButton.Checked = Default.UpdateWorker == DismApi;
    }

    protected override void OK_Button_Click(object sender, EventArgs e) {
        base.OK_Button_Click(sender, e);
        Default.AutoRestart = autoRestartBox.Checked;

        if (pkgmgrButton.Checked) {
            Default.UpdateWorker = PkgMgr;
        } else if (dismButton.Checked) {
            Default.UpdateWorker = Dism;
        } else if (dismapiButton.Checked) {
            Default.UpdateWorker = DismApi;
        }

        Default.Save();
    }
}