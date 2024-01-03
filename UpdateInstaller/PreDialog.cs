namespace UpdateInstaller;

public sealed partial class PreDialog {
    private readonly string preUpdatePath;
    private readonly Dictionary<string, string[]> preUpdates;

    public PreDialog() {
        InitializeComponent();
        HideOKButton();

        preUpdatePath = GetConfigValue("PreUpdatePath")!;
        preUpdates = new() {
            { GetConfigValue("PreUpdateDescription1") ?? "1", GetConfigValue("PreUpdate1")?.Split(',') ?? [] },
            { GetConfigValue("PreUpdateDescription2") ?? "2", GetConfigValue("PreUpdate2")?.Split(',') ?? [] },
            { GetConfigValue("PreUpdateDescription3") ?? "3", GetConfigValue("PreUpdate3")?.Split(',') ?? [] },
            { GetConfigValue("PreUpdateDescription4") ?? "4", GetConfigValue("PreUpdate4")?.Split(',') ?? [] },
            { GetConfigValue("PreUpdateDescription5") ?? "5", GetConfigValue("PreUpdate5")?.Split(',') ?? [] }
        };

        setEntry(1, preEntry1);
        setEntry(2, preEntry2);
        setEntry(3, preEntry3);
        setEntry(4, preEntry4);
        setEntry(5, preEntry5);

        void setEntry(int num, PreEntry entry) {
            var description = preUpdates.ToArray()[num - 1].Key;

            if (description != num.ToString()) {
                entry.Text = description;
            } else {
                entry.Visible = false;
            }
        }
    }

    private void entry_Click(object sender, EventArgs e) {
        OK_Button_Click(sender, e);

        new Progress((((PreEntry)sender).Name switch {
            nameof(preEntry1) => getPreUpdate(1),
            nameof(preEntry2) => getPreUpdate(2),
            nameof(preEntry3) => getPreUpdate(3),
            nameof(preEntry4) => getPreUpdate(4),
            nameof(preEntry5) => getPreUpdate(5),
            _ => throw new InvalidOperationException(),
        }).Select(u => Path.Combine(preUpdatePath + "_" + Arch, u + ".cab"))).Show();

        string[]? getPreUpdate(int index) => preUpdates.ToArray()[index - 1].Value;
    }
}
