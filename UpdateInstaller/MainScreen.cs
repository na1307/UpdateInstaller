using System.Diagnostics;

namespace UpdateInstaller;

public partial class MainScreen : UserControl {
    public MainScreen() => InitializeComponent();

    private void button1_Click(object sender, EventArgs e) {
        if (radioButton1.Checked) {
            throw new NotImplementedException();
        } else if (radioButton2.Checked) {
            throw new NotImplementedException();
        } else if (radioButton3.Checked) {
            throw new NotImplementedException();
        } else if (radioButton4.Checked) {
            Process.Start("explorer", ".");
        } else if (radioButton5.Checked) {
            MainForm.Instance.Close();
        } else {
            throw new InvalidOperationException();
        }
    }
}
