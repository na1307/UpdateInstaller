namespace UpdateInstaller;

public partial class Choose {
    public Choose() {
        InitializeComponent();
        radioButton1.Text = Package.Instance.GetUpdatePathDescription(1);

        if (Package.Instance.GetUpdatePath(UpdatePathType.Path2) != null) {
            radioButton2.Text = Package.Instance.GetUpdatePathDescription(2);
        } else {
            radioButton2.Visible = false;
        }

        if (Package.Instance.GetUpdatePath(UpdatePathType.Path3) != null) {
            radioButton3.Text = Package.Instance.GetUpdatePathDescription(3);
        } else {
            radioButton3.Visible = false;
        }

        if (Package.Instance.OSVersion != "6.1") {
            radioButton1.Enabled = !Winver.IsWindowsServer;
            radioButton2.Checked = !radioButton1.Enabled;
        }

        if (Package.Instance.OSVersion is "6.2" or "6.3" && Arch == "x86") {
            radioButton2.Enabled = false;
            radioButton3.Enabled = false;
        }
    }

    protected override void OK_Button_Click(object sender, EventArgs e) {
        base.OK_Button_Click(sender, e);

        string path;

        if (radioButton1.Checked) {
            path = Package.Instance.GetUpdatePath(UpdatePathType.Path1)!;
        } else if (radioButton2.Checked) {
            path = Package.Instance.GetUpdatePath(UpdatePathType.Path2)!;
        } else if (radioButton3.Checked) {
            path = Package.Instance.GetUpdatePath(UpdatePathType.Path3)!;
        } else {
            throw new InvalidOperationException();
        }

        // 기본 업데이트 경로
        var baseUpdatePath = path + "_" + Arch;

        // 추가 업데이트 경로
        var additionalUpdatePath = !Winver.IsWindowsServer ? Package.Instance.GetUpdatePath(UpdatePathType.Client) : Package.Instance.GetUpdatePath(UpdatePathType.Server);

        // 추가 업데이트 경로가 있으면
        if (additionalUpdatePath != null && Directory.Exists(additionalUpdatePath + "_" + Arch)) {
            additionalUpdatePath += "_" + Arch;
        }

        new Progress(Directory.GetFiles(baseUpdatePath, "*.cab").Concat(additionalUpdatePath != null ? Directory.GetFiles(additionalUpdatePath, "*.cab") : Enumerable.Empty<string>()).OrderBy(s => s.Split('\\').Last(), WinApiStrLogicalComparer.Shared)).Show();
    }
}