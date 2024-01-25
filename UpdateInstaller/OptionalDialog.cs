namespace UpdateInstaller;

public partial class OptionalDialog {
    private readonly OptionalUpdate[] updates = ConfigJsonFileHelper.OptionalUpdates?.Where(ou => ((ou.Arch & Arch) != 0) && ((ou.Platform & Platform) != 0)).Select(ou => ou with { Checked = false }).ToArray() ?? [];

    public OptionalDialog() {
        InitializeComponent();
        Opacity = 0;
        dataGridView1.AutoGenerateColumns = false;
        dataGridView1.DataSource = updates;
    }

    protected override void OnLoad(EventArgs e) {
        base.OnLoad(e);

        if (updates.Length == 0) {
            BeginInvoke(Close);
            MessageBox.Show("선택적 업데이트가 없습니다.", "선택적 업데이트 설치", MessageBoxButtons.OK, MessageBoxIcon.Information);
        } else {
            Opacity = 1;
        }
    }

    protected override void OK_Button_Click(object sender, EventArgs e) {
        IEnumerable<OptionalUpdate> checkes = updates.Where(ou => ou.Checked);

        if (checkes.Any()) {
            new Progress(checkes.Cast<Update>()).Show();
            base.OK_Button_Click(sender, e);
        } else {
            MessageBox.Show("선택한 업데이트가 없습니다.", "선택적 업데이트 설치", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.Cancel;
        }
    }
}
