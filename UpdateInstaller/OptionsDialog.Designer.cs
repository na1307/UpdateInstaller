namespace UpdateInstaller;

partial class OptionsDialog : Dialog {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

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
            this.autoRestartBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pkgmgrButton = new System.Windows.Forms.RadioButton();
            this.dismButton = new System.Windows.Forms.RadioButton();
            this.dismapiButton = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // autoRestartBox
            // 
            this.autoRestartBox.Location = new System.Drawing.Point(12, 12);
            this.autoRestartBox.Name = "autoRestartBox";
            this.autoRestartBox.Size = new System.Drawing.Size(260, 25);
            this.autoRestartBox.TabIndex = 1;
            this.autoRestartBox.Text = "자동 재시작";
            this.autoRestartBox.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pkgmgrButton);
            this.groupBox1.Controls.Add(this.dismButton);
            this.groupBox1.Controls.Add(this.dismapiButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 181);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "패키지 설치 프로그램";
            // 
            // pkgmgrButton
            // 
            this.pkgmgrButton.AutoSize = true;
            this.pkgmgrButton.Location = new System.Drawing.Point(6, 22);
            this.pkgmgrButton.Name = "pkgmgrButton";
            this.pkgmgrButton.Size = new System.Drawing.Size(67, 19);
            this.pkgmgrButton.TabIndex = 0;
            this.pkgmgrButton.TabStop = true;
            this.pkgmgrButton.Text = "PkgMgr";
            this.pkgmgrButton.UseVisualStyleBackColor = true;
            // 
            // dismButton
            // 
            this.dismButton.AutoSize = true;
            this.dismButton.Location = new System.Drawing.Point(6, 47);
            this.dismButton.Name = "dismButton";
            this.dismButton.Size = new System.Drawing.Size(53, 19);
            this.dismButton.TabIndex = 1;
            this.dismButton.TabStop = true;
            this.dismButton.Text = "Dism";
            this.dismButton.UseVisualStyleBackColor = true;
            // 
            // dismapiButton
            // 
            this.dismapiButton.AutoSize = true;
            this.dismapiButton.Location = new System.Drawing.Point(6, 72);
            this.dismapiButton.Name = "dismapiButton";
            this.dismapiButton.Size = new System.Drawing.Size(75, 19);
            this.dismapiButton.TabIndex = 2;
            this.dismapiButton.TabStop = true;
            this.dismapiButton.Text = "Dism API";
            this.dismapiButton.UseVisualStyleBackColor = true;
            // 
            // OptionsDialog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.autoRestartBox);
            this.Name = "OptionsDialog";
            this.Text = "옵션";
            this.Controls.SetChildIndex(this.autoRestartBox, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private CheckBox autoRestartBox;
    private GroupBox groupBox1;
    private RadioButton pkgmgrButton;
    private RadioButton dismButton;
    private RadioButton dismapiButton;
}