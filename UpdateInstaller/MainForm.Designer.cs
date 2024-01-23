namespace UpdateInstaller;

partial class MainForm : Form {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem000 = new System.Windows.Forms.MenuItem();
            this.menuItem001 = new System.Windows.Forms.MenuItem();
            this.menuItem100 = new System.Windows.Forms.MenuItem();
            this.menuItem101 = new System.Windows.Forms.MenuItem();
            this.menuItem200 = new System.Windows.Forms.MenuItem();
            this.menuItem201 = new System.Windows.Forms.MenuItem();
            this.menuItem202 = new System.Windows.Forms.MenuItem();
            this.menuItem203 = new System.Windows.Forms.MenuItem();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(310, 50);
            this.button1.TabIndex = 0;
            this.button1.Text = "설치";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 68);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(152, 25);
            this.button2.TabIndex = 1;
            this.button2.Text = "사전 업데이트";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 99);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(310, 25);
            this.button3.TabIndex = 2;
            this.button3.Text = "찾아보기";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem000,
            this.menuItem100,
            this.menuItem200});
            // 
            // menuItem000
            // 
            this.menuItem000.Index = 0;
            this.menuItem000.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem001});
            this.menuItem000.Text = "파일(&F)";
            // 
            // menuItem001
            // 
            this.menuItem001.Index = 0;
            this.menuItem001.Text = "끝내기(&X)";
            this.menuItem001.Click += new System.EventHandler(this.menuItem001_Click);
            // 
            // menuItem100
            // 
            this.menuItem100.Index = 1;
            this.menuItem100.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem101});
            this.menuItem100.Text = "도구(&T)";
            // 
            // menuItem101
            // 
            this.menuItem101.Index = 0;
            this.menuItem101.Text = "옵션(&O)";
            this.menuItem101.Click += new System.EventHandler(this.menuItem101_Click);
            // 
            // menuItem200
            // 
            this.menuItem200.Index = 2;
            this.menuItem200.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem201,
            this.menuItem202,
            this.menuItem203});
            this.menuItem200.Text = "도움말(&H)";
            // 
            // menuItem201
            // 
            this.menuItem201.Index = 0;
            this.menuItem201.Text = "도움말 보기(&V)";
            this.menuItem201.Click += new System.EventHandler(this.menuItem201_Click);
            // 
            // menuItem202
            // 
            this.menuItem202.Index = 1;
            this.menuItem202.Text = "-";
            // 
            // menuItem203
            // 
            this.menuItem203.Index = 2;
            this.menuItem203.Text = "{0} 정보(&A)";
            this.menuItem203.Click += new System.EventHandler(this.menuItem203_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(170, 68);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(152, 25);
            this.button4.TabIndex = 3;
            this.button4.Text = "선택적 업데이트";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(334, 130);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Menu = this.mainMenu1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

    }

    #endregion

    private Button button1;
    private Button button2;
    private Button button3;
    private MainMenu mainMenu1;
    private MenuItem menuItem000;
    private MenuItem menuItem001;
    private MenuItem menuItem100;
    private MenuItem menuItem101;
    private MenuItem menuItem200;
    private MenuItem menuItem201;
    private MenuItem menuItem202;
    private MenuItem menuItem203;
    private Button button4;
}