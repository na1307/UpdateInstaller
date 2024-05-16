namespace UpdateInstaller;

public partial class MainForm : Form {
    private static MainForm? _instance;

    private MainForm() {
        InitializeComponent();
        Screen = new MainScreen();
    }

    public static MainForm Instance {
        get {
            _instance ??= new();

            return _instance;
        }
    }

    public UserControl Screen {
        get => (UserControl)panel1.Controls[0];
        set {
            panel1.Controls.Clear();
            panel1.Controls.Add(value);
        }
    }
}
