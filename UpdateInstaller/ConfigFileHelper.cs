using System.Xml;

namespace UpdateInstaller;

public static class ConfigFileHelper {
    private static readonly XmlNode packageNode = getPackageNode();

    public static string? GetConfigValue(string path) => packageNode.SelectSingleNode(path)?.InnerText;

    private static XmlNode getPackageNode() {
        XmlDocument xml = new() { XmlResolver = null };
        xml.Load(ConfigFileName);

        return xml.SelectSingleNode("Package");
    }
}
