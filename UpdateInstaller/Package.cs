using System.Xml;

namespace UpdateInstaller;

public sealed class Package {
    private const string fileName = "UpdateInstaller.xml";
    private readonly Dictionary<UpdatePathType, string?> updatePaths;
    private readonly List<string?> updatePathDescriptions;
    private readonly Dictionary<string, string[]?> preUpdates;

    public static Package Instance { get; } = new();

    public string OSVersion { get; }
    public string SPVersion { get; }
    public string PackageVersion { get; }

    private Package() {
        XmlDocument xml = new() { XmlResolver = null };

        try {
            xml.Load(fileName);
        } catch (FileNotFoundException ex) {
            throw new LoadException("구성 파일을 찾을 수 없습니다.", ex);
        }

        XmlNode packagenode = xml.SelectSingleNode("Package");

        // 필수 구성이 없으면 예외 던지기
        IEnumerable<string> optitems = new[] { nameof(OSVersion), nameof(SPVersion), nameof(PackageVersion) }.Where(req => packagenode.SelectSingleNode(req) == null);
        if (optitems.Any()) throw new LoadException($"구성 파일에 \"{optitems.First()}\" 항목이 없습니다.");

        OSVersion = packagenode.SelectSingleNode(nameof(OSVersion)).InnerText;
        SPVersion = packagenode.SelectSingleNode(nameof(SPVersion)).InnerText;
        PackageVersion = packagenode.SelectSingleNode(nameof(PackageVersion)).InnerText;

        updatePaths = new() {
            { UpdatePathType.Path1, packagenode.SelectSingleNode("UpdatePath1")?.InnerText },
            { UpdatePathType.Path2, packagenode.SelectSingleNode("UpdatePath2")?.InnerText },
            { UpdatePathType.Path3, packagenode.SelectSingleNode("UpdatePath3")?.InnerText },
            { UpdatePathType.Client, packagenode.SelectSingleNode("ClientUpdatePath")?.InnerText },
            { UpdatePathType.Server, packagenode.SelectSingleNode("ServerUpdatePath")?.InnerText },
            { UpdatePathType.Pre, packagenode.SelectSingleNode("PreUpdatePath")?.InnerText }
        };

        updatePathDescriptions = [
            packagenode.SelectSingleNode("UpdatePathDescription1")?.InnerText,
            packagenode.SelectSingleNode("UpdatePathDescription2")?.InnerText,
            packagenode.SelectSingleNode("UpdatePathDescription3")?.InnerText,
        ];

        preUpdates = new() {
            { packagenode.SelectSingleNode("PreUpdateDescription1")?.InnerText ?? "1", packagenode.SelectSingleNode("PreUpdate1")?.InnerText.Split(',') },
            { packagenode.SelectSingleNode("PreUpdateDescription2")?.InnerText ?? "2", packagenode.SelectSingleNode("PreUpdate2")?.InnerText.Split(',') },
            { packagenode.SelectSingleNode("PreUpdateDescription3")?.InnerText ?? "3", packagenode.SelectSingleNode("PreUpdate3")?.InnerText.Split(',') },
            { packagenode.SelectSingleNode("PreUpdateDescription4")?.InnerText ?? "4", packagenode.SelectSingleNode("PreUpdate4")?.InnerText.Split(',') },
            { packagenode.SelectSingleNode("PreUpdateDescription5")?.InnerText ?? "5", packagenode.SelectSingleNode("PreUpdate5")?.InnerText.Split(',') }
        };
#if !DEBUG

        // 커널이나 서비스 팩 버전이 다름
        if (OSVersion != Environment.OSVersion.Version.ToString(2) || SPVersion != Winver.SPLevel.ToString()) throw new LoadException("패키지와 현재 운영 체제가 호환되지 않습니다.");
#endif
    }

    public string? GetUpdatePath(UpdatePathType type) => updatePaths[type];
    public string? GetUpdatePathDescription(int index) => updatePathDescriptions[index - 1];
    public string[]? GetPreUpdate(int index) => preUpdates.ToArray()[index - 1].Value;
    public string GetPreUpdateDescription(int index) => preUpdates.ToArray()[index - 1].Key;

    private sealed class LoadException : UpdateInstallerException {
        public LoadException(string message) : base(message) { }
        public LoadException(string message, Exception inner) : base(message, inner) { }
    }
}
