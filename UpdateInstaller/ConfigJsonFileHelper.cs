using Newtonsoft.Json.Linq;

namespace UpdateInstaller;

public static class ConfigJsonFileHelper {
    private static readonly JObject jobj = JObject.Parse(File.ReadAllText(ConfigFileName));
    private static readonly UpdatePath[] updatePaths = jobj["UpdatePaths"]!.ToObject<UpdatePath[]>()!;
    private static readonly PreUpdate[] preUpdates = jobj["PreUpdates"]!.ToObject<PreUpdate[]>()!;

    public static string? OSVersion { get; } = getConfigValue(nameof(OSVersion));
    public static string? SPVersion { get; } = getConfigValue(nameof(SPVersion));
    public static string? PackageVersion { get; } = getConfigValue(nameof(PackageVersion));
    public static string? ClientUpdatePath { get; } = getConfigValue(nameof(ClientUpdatePath));
    public static string? ServerUpdatePath { get; } = getConfigValue(nameof(ServerUpdatePath));
    public static string? PreUpdatePath { get; } = getConfigValue(nameof(PreUpdatePath));

    public static UpdatePath? GetUpdatePath(int index) => updatePaths.ElementAtOrDefault(index - 1);
    public static PreUpdate? GetPreUpdate(int index) => preUpdates.ElementAtOrDefault(index - 1);

    private static string? getConfigValue(string path) => jobj[path]?.ToString();
}
