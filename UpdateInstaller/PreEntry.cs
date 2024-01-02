namespace UpdateInstaller;

[DefaultEvent(nameof(Install))]
public partial class PreEntry {
    public PreEntry() {
        InitializeComponent();
        button1.Click += (_, _) => Install?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler? Install;

    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Bindable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override string Text {
        get => label1.Text;
        set {
            base.Text = value;
            label1.Text = value;
        }
    }
}
