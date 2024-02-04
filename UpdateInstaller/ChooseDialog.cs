using System.Runtime.CompilerServices;
using static UpdateInstaller.ConfigJsonFileHelper;

namespace UpdateInstaller;

public partial class ChooseDialog {
    public ChooseDialog() {
        InitializeComponent();
        setDescription(4, radioButton4);
        setDescription(3, radioButton3);
        setDescription(2, radioButton2);
        setDescription(1, radioButton1);

        static void setDescription(int index, RadioButton radioButton) {
            UpdatePathItem? updatePath = getUpdatePath(index);

            if (updatePath != null && Directory.Exists(updatePath.Path + "_" + Arch)) {
                radioButton.Text = updatePath.Description;

                if ((updatePath.Arch != CpuArch.All && ((updatePath.Arch & Arch) == 0)) || (updatePath.Platform != OSPlatform.Both && ((updatePath.Platform & Platform) == 0))) {
                    radioButton.Enabled = false;
                    radioButton.Checked = false;
                } else {
                    radioButton.Checked = true;
                }
            } else {
                radioButton.Visible = false;
                radioButton.Checked = false;
            }
        }
    }

    protected override void OK_Button_Click(object sender, EventArgs e) {
        base.OK_Button_Click(sender, e);

        string path;

        if (radioButton1.Checked) {
            path = getUpdatePath(1)!.Path;
        } else if (radioButton2.Checked) {
            path = getUpdatePath(2)!.Path;
        } else if (radioButton3.Checked) {
            path = getUpdatePath(3)!.Path;
        } else if (radioButton4.Checked) {
            path = getUpdatePath(4)!.Path;
        } else {
            throw new InvalidOperationException();
        }

        // 업데이트들
        IEnumerable<Update> @base = Directory.GetFiles(path + "_" + Arch, "*.cab").Select(f => new Update(f));
        IEnumerable<Update> add = Enumerable.Empty<Update>();

        var additionalUpdatePath = Platform == OSPlatform.Client ? ClientUpdatePath : ServerUpdatePath;

        // 추가 업데이트 경로가 있으면
        if (!string.IsNullOrEmpty(additionalUpdatePath) && Directory.Exists(additionalUpdatePath + "_" + Arch)) {
            additionalUpdatePath += "_" + Arch;
            add = Directory.GetFiles(additionalUpdatePath, "*.cab").Select(f => new Update(f));
        }

        new Progress(@base.Concat(add).OrderBy(u => u.FullPath.Split('\\').Last(), WinApiStrLogicalComparer.Shared)).Show();
    }

    [MethodImpl(AggressiveInlining)]
    private static UpdatePathItem? getUpdatePath(int index) => UpdatePaths.ElementAtOrDefault(index);
}
