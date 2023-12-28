#nullable disable
namespace UpdateInstaller.Features;

public class Dialog : Form {
    private TableLayoutPanel tableLayoutPanel1;
    private Button okButton;
#pragma warning disable S1450
    private Button cancelButton;
#pragma warning restore S1450

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private readonly IContainer components = null;

#nullable enable
    protected Dialog() => InitializeComponent();

    protected override void OnLayout(LayoutEventArgs levent) {
        base.OnLayout(levent);

        const int s =
#if NET45_OR_GREATER || !NETFRAMEWORK
    10
#else
0
#endif
;
        const int w = 190 + s;
        const int h = 70 + s;

        tableLayoutPanel1.Location = new(Size.Width - w, Size.Height - h);
    }

    protected virtual void OK_Button_Click(object sender, EventArgs e) {
        DialogResult = DialogResult.OK;
    }

    protected virtual void Cancel_Button_Click(object sender, EventArgs e) {
        DialogResult = DialogResult.Cancel;
    }

    protected void HideOKButton() => okButton.Hide();
#nullable disable

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
        tableLayoutPanel1 = new TableLayoutPanel();
        okButton = new Button();
        cancelButton = new Button();
        tableLayoutPanel1.SuspendLayout();
        SuspendLayout();
        //
        // tableLayoutPanel1
        //
        tableLayoutPanel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        tableLayoutPanel1.ColumnCount = 2;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.Controls.Add(okButton, 0, 0);
        tableLayoutPanel1.Controls.Add(cancelButton, 1, 0);
        tableLayoutPanel1.Location = new Point(101, 218);
        tableLayoutPanel1.Margin = new Padding(4, 3, 4, 3);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 1;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.Size = new Size(170, 31);
        tableLayoutPanel1.TabIndex = 0;
        //
        // okButton
        //
        okButton.Anchor = AnchorStyles.None;
        okButton.DialogResult = DialogResult.OK;
        okButton.Location = new Point(4, 3);
        okButton.Margin = new Padding(4, 3, 4, 3);
        okButton.Name = "okButton";
        okButton.Size = new Size(77, 25);
        okButton.TabIndex = 0;
        okButton.Text = "확인";
        okButton.UseVisualStyleBackColor = true;
        okButton.Click += new EventHandler(OK_Button_Click);
        //
        // cancelButton
        //
        cancelButton.Anchor = AnchorStyles.None;
        cancelButton.DialogResult = DialogResult.Cancel;
        cancelButton.Location = new Point(89, 3);
        cancelButton.Margin = new Padding(4, 3, 4, 3);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new Size(77, 25);
        cancelButton.TabIndex = 1;
        cancelButton.Text = "취소";
        cancelButton.UseVisualStyleBackColor = true;
        cancelButton.Click += new EventHandler(Cancel_Button_Click);
        //
        // Dialog
        //
        AcceptButton = okButton;
        AutoScaleMode = AutoScaleMode.None;
        CancelButton = cancelButton;
        ClientSize = new Size(284, 261);
        Controls.Add(tableLayoutPanel1);
        Font = new Font("맑은 고딕", 9F);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "Dialog";
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterScreen;
        TopMost = true;
        tableLayoutPanel1.ResumeLayout(false);
        ResumeLayout(false);

    }

    #endregion
}
