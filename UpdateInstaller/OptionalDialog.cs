namespace UpdateInstaller;

public partial class OptionalDialog {
    private readonly OptionalUpdate[] updates = ConfigJsonFileHelper.OptionalUpdates.Select(ou => ou with { Checked = false }).ToArray();

    public OptionalDialog() {
        InitializeComponent();
        dataGridView1.AutoGenerateColumns = false;
        dataGridView1.DataSource = updates;
    }

    protected override void OK_Button_Click(object sender, EventArgs e) {
        new Progress(updates.Where(ou => ou.Checked).Cast<Update>()).Show();
        base.OK_Button_Click(sender, e);
    }
}
