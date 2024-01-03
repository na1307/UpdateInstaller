namespace UpdateInstaller;

partial class PreDialog : Dialog {
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
            this.preEntry1 = new UpdateInstaller.PreEntry();
            this.preEntry2 = new UpdateInstaller.PreEntry();
            this.preEntry3 = new UpdateInstaller.PreEntry();
            this.preEntry4 = new UpdateInstaller.PreEntry();
            this.preEntry5 = new UpdateInstaller.PreEntry();
            this.SuspendLayout();
            // 
            // preEntry1
            // 
            this.preEntry1.Location = new System.Drawing.Point(12, 12);
            this.preEntry1.Name = "preEntry1";
            this.preEntry1.Size = new System.Drawing.Size(581, 25);
            this.preEntry1.TabIndex = 1;
            this.preEntry1.Text = "description1";
            this.preEntry1.Install += new System.EventHandler(this.entry_Click);
            // 
            // preEntry2
            // 
            this.preEntry2.Location = new System.Drawing.Point(12, 43);
            this.preEntry2.Name = "preEntry2";
            this.preEntry2.Size = new System.Drawing.Size(581, 25);
            this.preEntry2.TabIndex = 2;
            this.preEntry2.Text = "description2";
            this.preEntry2.Install += new System.EventHandler(this.entry_Click);
            // 
            // preEntry3
            // 
            this.preEntry3.Location = new System.Drawing.Point(12, 74);
            this.preEntry3.Name = "preEntry3";
            this.preEntry3.Size = new System.Drawing.Size(581, 25);
            this.preEntry3.TabIndex = 3;
            this.preEntry3.Text = "description3";
            this.preEntry3.Install += new System.EventHandler(this.entry_Click);
            // 
            // preEntry4
            // 
            this.preEntry4.Location = new System.Drawing.Point(12, 105);
            this.preEntry4.Name = "preEntry4";
            this.preEntry4.Size = new System.Drawing.Size(581, 25);
            this.preEntry4.TabIndex = 4;
            this.preEntry4.Text = "description4";
            this.preEntry4.Install += new System.EventHandler(this.entry_Click);
            // 
            // preEntry5
            // 
            this.preEntry5.Location = new System.Drawing.Point(12, 136);
            this.preEntry5.Name = "preEntry5";
            this.preEntry5.Size = new System.Drawing.Size(581, 25);
            this.preEntry5.TabIndex = 5;
            this.preEntry5.Text = "description5";
            this.preEntry5.Install += new System.EventHandler(this.entry_Click);
            // 
            // Pre
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(605, 208);
            this.Controls.Add(this.preEntry5);
            this.Controls.Add(this.preEntry4);
            this.Controls.Add(this.preEntry3);
            this.Controls.Add(this.preEntry2);
            this.Controls.Add(this.preEntry1);
            this.Name = "Pre";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "사전 업데이트 설치";
            this.Controls.SetChildIndex(this.preEntry1, 0);
            this.Controls.SetChildIndex(this.preEntry2, 0);
            this.Controls.SetChildIndex(this.preEntry3, 0);
            this.Controls.SetChildIndex(this.preEntry4, 0);
            this.Controls.SetChildIndex(this.preEntry5, 0);
            this.ResumeLayout(false);

    }

    #endregion

    private PreEntry preEntry1;
    private PreEntry preEntry2;
    private PreEntry preEntry3;
    private PreEntry preEntry4;
    private PreEntry preEntry5;
}