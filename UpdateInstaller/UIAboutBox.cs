using static Bluehill.AssemblyProperties;

namespace UpdateInstaller;

public sealed partial class UIAboutBox {
    public UIAboutBox() {
        InitializeComponent();
        Text = $"{AssemblyTitle} 정보";
        labelProductName.Text = AssemblyProduct;
        labelVersion.Text = $"버전 {AssemblyInformationalVersion} (빌드 {BuildNumber})";
        labelCopyright.Text = AssemblyCopyright;
        labelCompanyName.Text = AssemblyCompany;
        textBoxDescription.Text = AssemblyDescription;
        okButton.Click += (_, _) => Close();
    }
}