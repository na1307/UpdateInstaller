using static UpdateInstaller.ConfigJsonFileHelper;

namespace UpdateInstaller;

public sealed partial class PreDialog {
    public PreDialog() {
        InitializeComponent();
        HideOKButton();
        setDescription(1, preEntry1);
        setDescription(2, preEntry2);
        setDescription(3, preEntry3);
        setDescription(4, preEntry4);
        setDescription(5, preEntry5);

        static void setDescription(int index, PreEntry entry) {
            PreUpdate? preUpdate = GetPreUpdate(index);

            if (preUpdate is not null) {
                entry.Text = preUpdate.Description;

                if (preUpdate.Arch is not null && !preUpdate.Arch.Contains(Arch)) {
                    entry.Enabled = false;
                }
            } else {
                entry.Visible = false;
            }
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
        }).Updates.Select(u => new Update(Path.Combine(PreUpdatePath + "_" + Arch, u + ".cab")))).Show();
    }
}
