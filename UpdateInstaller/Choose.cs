namespace UpdateInstaller;

public partial class Choose {
    private readonly string?[] updatePaths;
    private readonly string? clientUpdatePath;
    private readonly string? serverUpdatePath;
    private readonly string?[] updatePathDescriptions;

    public Choose() {
        InitializeComponent();

        updatePaths = [
            GetConfigValue("UpdatePath1"),
            GetConfigValue("UpdatePath2"),
            GetConfigValue("UpdatePath3"),
        ];

        clientUpdatePath = GetConfigValue("ClientUpdatePath");
        serverUpdatePath = GetConfigValue("ServerUpdatePath");

        updatePathDescriptions = [
            GetConfigValue("UpdatePathDescription1"),
            GetConfigValue("UpdatePathDescription2"),
            GetConfigValue("UpdatePathDescription3"),
        ];

        radioButton1.Text = getUpdatePathDescription(1);

        if (getUpdatePath(2) != null) {
            radioButton2.Text = getUpdatePathDescription(2);
        } else {
            radioButton2.Visible = false;
        }

        if (getUpdatePath(3) != null) {
            radioButton3.Text = getUpdatePathDescription(3);
        } else {
            radioButton3.Visible = false;
        }

        if (GetConfigValue(OSVersion) != "6.1") {
            radioButton1.Enabled = !Winver.IsWindowsServer;
            radioButton2.Checked = !radioButton1.Enabled;
        }

        if (GetConfigValue(OSVersion) is "6.2" or "6.3" && Arch == "x86") {
            radioButton2.Enabled = false;
            radioButton3.Enabled = false;
        }

        string? getUpdatePathDescription(int index) => updatePathDescriptions[index - 1];
    }

    protected override void OK_Button_Click(object sender, EventArgs e) {
        base.OK_Button_Click(sender, e);

        string path;

        if (radioButton1.Checked) {
            path = getUpdatePath(1)!;
        } else if (radioButton2.Checked) {
            path = getUpdatePath(2)!;
        } else if (radioButton3.Checked) {
            path = getUpdatePath(3)!;
        } else {
            throw new InvalidOperationException();
        }

        // 기본 업데이트 경로
        var baseUpdatePath = path + "_" + Arch;

        // 추가 업데이트 경로
        var additionalUpdatePath = !Winver.IsWindowsServer ? clientUpdatePath : serverUpdatePath;

        // 추가 업데이트 경로가 있으면
        if (additionalUpdatePath != null && Directory.Exists(additionalUpdatePath + "_" + Arch)) {
            additionalUpdatePath += "_" + Arch;
        }

        new Progress(Directory.GetFiles(baseUpdatePath, "*.cab").Concat(additionalUpdatePath != null ? Directory.GetFiles(additionalUpdatePath, "*.cab") : Enumerable.Empty<string>()).OrderBy(s => s.Split('\\').Last(), WinApiStrLogicalComparer.Shared)).Show();
    }

    private string? getUpdatePath(int index) => updatePaths[index - 1];
}