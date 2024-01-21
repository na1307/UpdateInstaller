using static UpdateInstaller.ConfigJsonFileHelper;

namespace UpdateInstaller;

public partial class ChooseDialog {
    public ChooseDialog() {
        InitializeComponent();

        radioButton1.Text = GetUpdatePath(1)!.Description;

        setDescription(2, radioButton2);
        setDescription(3, radioButton3);
        setDescription(4, radioButton4);

        static void setDescription(int index, RadioButton radioButton) {
            UpdatePathItem? updatePath = GetUpdatePath(index);

            if (updatePath != null) {
                radioButton.Text = updatePath.Description;

                if ((updatePath.Arch != CpuArch.All && ((updatePath.Arch & Arch) == 0)) || !Directory.Exists(updatePath.Path + "_" + Arch)) {
                    radioButton.Enabled = false;
                }
            } else {
                radioButton.Visible = false;
            }
        }
    }

    protected override void OK_Button_Click(object sender, EventArgs e) {
        base.OK_Button_Click(sender, e);

        string path;

        if (radioButton1.Checked) {
            path = GetUpdatePath(1)!.Path;
        } else if (radioButton2.Checked) {
            path = GetUpdatePath(2)!.Path;
        } else if (radioButton3.Checked) {
            path = GetUpdatePath(3)!.Path;
        } else if (radioButton4.Checked) {
            path = GetUpdatePath(4)!.Path;
        } else {
            throw new InvalidOperationException();
        }

        // 기본 업데이트 경로
        var baseUpdatePath = path + "_" + Arch;

        // 추가 업데이트 경로
        var additionalUpdatePath = !Winver.IsWindowsServer ? ClientUpdatePath : ServerUpdatePath;

        // 추가 업데이트 경로가 있으면
        if (additionalUpdatePath != null && Directory.Exists(additionalUpdatePath + "_" + Arch)) {
            additionalUpdatePath += "_" + Arch;
        }

        IEnumerable<Update> @base = Directory.GetFiles(baseUpdatePath, "*.cab").Select(f => new Update(f));
        IEnumerable<Update> add = additionalUpdatePath != null ? Directory.GetFiles(additionalUpdatePath, "*.cab").Select(f => new Update(f)) : Enumerable.Empty<Update>();

        new Progress(@base.Concat(add).OrderBy(u => u.FullPath.Split('\\').Last(), WinApiStrLogicalComparer.Shared)).Show();
    }
}
