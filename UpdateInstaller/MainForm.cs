using static UpdateInstaller.ConfigJsonFileHelper;

namespace UpdateInstaller;

public sealed partial class MainForm {
    private static readonly Lazy<MainForm> _Instance = new(() => new());

    private MainForm() {
        InitializeComponent();
        menuItem203.Text = string.Format(menuItem203.Text, AssemblyProperties.AssemblyTitle);

        Text = (OSVersion switch {
            Vista => "Windows Vista / Server 2008",
            Seven => "Windows 7 / Server 2008 R2",
            Eight => "Windows 8 / Server 2012",
            Blue => "Windows 8.1 / Server 2012 R2",
            _ => throw new InvalidOperationException(),
        }) + " Update Package " + PackageVersion.ToString("F1");
    }

    public static MainForm Instance => _Instance.Value;

    protected override void OnActivated(EventArgs e) {
        base.OnActivated(e);

        if (Status.SystemShutdown) {
            Close();
        }

        if (Status.MustRestart) {
            button1.Enabled = false;
            button2.Enabled = false;
        }
    }

    protected override void OnFormClosing(FormClosingEventArgs e) {
        base.OnFormClosing(e);

        if (!Properties.Settings.Default.AutoRestart && Status.MustRestart && !Status.SystemShutdown) {
            switch (MessageBox.Show("업데이트 설치를 완료하려면 다시 시작해야 합니다.\r\n\r\n지금 다시 시작 할까요?", "끝내기", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)) {
                case DialogResult.Yes:
                    RestartHelper.Restart();
                    break;

                case DialogResult.No:
                    break;

                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;

                default:
                    throw new InvalidOperationException();
            }
        }
    }

    protected override void WndProc(ref Message m) {
        if (m.Msg == WM_QUERYENDSESSION) {
            Status.SystemShutdown = true;
        }

        base.WndProc(ref m);
    }

    private void button1_Click(object sender, EventArgs e) {
        if (new ChooseDialog().ShowDialog() == DialogResult.OK) Hide();
    }

    private void button2_Click(object sender, EventArgs e) {
        if (new PreDialog().ShowDialog() == DialogResult.OK) Hide();
    }

    private void button3_Click(object sender, EventArgs e) {
        Process.Start("explorer", Application.StartupPath);
    }

    private void menuItem001_Click(object sender, EventArgs e) {
        Close();
    }

    private void menuItem101_Click(object sender, EventArgs e) {
        new OptionsDialog().ShowDialog();
    }

    private void menuItem201_Click(object sender, EventArgs e) {
        Process.Start("readme.html");
    }

    private void menuItem203_Click(object sender, EventArgs e) {
        new UIAboutBox().ShowDialog();
    }
}
