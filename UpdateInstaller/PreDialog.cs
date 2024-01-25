using static UpdateInstaller.ConfigJsonFileHelper;

namespace UpdateInstaller;

public sealed partial class PreDialog {
    public PreDialog() {
        InitializeComponent();
        Opacity = 0;
        HideOKButton();
        setDescription(1, preEntry1);
        setDescription(2, preEntry2);
        setDescription(3, preEntry3);
        setDescription(4, preEntry4);
        setDescription(5, preEntry5);

        static void setDescription(int index, PreEntry entry) {
            PreUpdateItem? preUpdate = GetPreUpdate(index);

            if (preUpdate is not null) {
                entry.Text = preUpdate.Description;
            } else {
                entry.Visible = false;
            }
        }
    }

    protected override void OnLoad(EventArgs e) {
        base.OnLoad(e);

        if (GetPreUpdate(1) == null) {
            BeginInvoke(Close);
            MessageBox.Show("사전 업데이트가 없습니다.", "사전 업데이트 설치", MessageBoxButtons.OK, MessageBoxIcon.Information);
        } else {
            Opacity = 1;
        }
    }

    private void entry_Click(object sender, EventArgs e) {
        OK_Button_Click(sender, e);

        new Progress((((PreEntry)sender).Name switch {
            nameof(preEntry1) => GetPreUpdate(1)!,
            nameof(preEntry2) => GetPreUpdate(2)!,
            nameof(preEntry3) => GetPreUpdate(3)!,
            nameof(preEntry4) => GetPreUpdate(4)!,
            nameof(preEntry5) => GetPreUpdate(5)!,
            _ => throw new InvalidOperationException(),
        }).Updates).Show();
    }
}
