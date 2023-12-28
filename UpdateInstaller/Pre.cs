namespace UpdateInstaller;

public sealed partial class Pre {
    public Pre() {
        InitializeComponent();
        HideOKButton();

        var temp1 = Package.Instance.GetPreUpdateDescription(1);

        if (temp1 != "1") {
            preEntry1.Text = temp1;
        } else {
            preEntry1.Visible = false;
        }

        var temp2 = Package.Instance.GetPreUpdateDescription(2);

        if (temp2 != "2") {
            preEntry2.Text = temp2;
        } else {
            preEntry2.Visible = false;
        }

        var temp3 = Package.Instance.GetPreUpdateDescription(3);

        if (temp3 != "3") {
            preEntry3.Text = temp3;
        } else {
            preEntry3.Visible = false;
        }

        var temp4 = Package.Instance.GetPreUpdateDescription(4);

        if (temp4 != "4") {
            preEntry4.Text = temp4;
        } else {
            preEntry4.Visible = false;
        }

        var temp5 = Package.Instance.GetPreUpdateDescription(5);

        if (temp5 != "5") {
            preEntry5.Text = temp5;
        } else {
            preEntry5.Visible = false;
        }
    }

    private void entry_Click(object sender, EventArgs e) {
        OK_Button_Click(sender, e);

        new Progress((((PreEntry)sender).Name switch {
            nameof(preEntry1) => Package.Instance.GetPreUpdate(1),
            nameof(preEntry2) => Package.Instance.GetPreUpdate(2),
            nameof(preEntry3) => Package.Instance.GetPreUpdate(3),
            nameof(preEntry4) => Package.Instance.GetPreUpdate(4),
            nameof(preEntry5) => Package.Instance.GetPreUpdate(5),
            _ => throw new InvalidOperationException(),
        }).Select(u => Path.Combine(Package.Instance.GetUpdatePath(UpdatePathType.Pre) + "_" + Arch, u + ".cab"))).Show();
    }
}
