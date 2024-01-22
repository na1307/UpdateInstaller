using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System.Reflection;

namespace UpdateInstaller;

public static class ConfigJsonFileHelper {
    private static readonly string json = File.ReadAllText(ConfigFileName);
    private static readonly JObject jobj = JObject.Parse(json);
    private static readonly DeserializedConfig deserialized = JsonConvert.DeserializeObject<DeserializedConfig>(json)!;
    private static readonly UpdatePathItem[] updatePathItems = JsonConvert.DeserializeObject<UpdatePathItem[]>(jobj["UpdatePaths"]!.ToString())!;
    private static readonly PreUpdateItem[]? preUpdateItems = JsonConvert.DeserializeObject<PreUpdateItem[]>(jobj["PreUpdates"]?.ToString() ?? string.Empty, new PreUpdateItemsConverter());

    public static double OSVersion => deserialized.OSVersion;
    public static int SPVersion => deserialized.SPVersion;
    public static double PackageVersion => deserialized.PackageVersion;
    public static string? ClientUpdatePath => deserialized.ClientUpdatePath;
    public static string? ServerUpdatePath => deserialized.ServerUpdatePath;
    public static string? PreUpdatePath => deserialized.PreUpdatePath;

    public static bool IsConfigJsonValid {
        get {
            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("UpdateInstaller.UpdateInstaller.schema.json");
            using StreamReader reader = new StreamReader(stream);

            return jobj.IsValid(JsonSchema.Parse(reader.ReadToEnd()));
        }
    }

    public static UpdatePathItem? GetUpdatePath(int index) => updatePathItems.ElementAtOrDefault(index - 1);
    public static PreUpdateItem? GetPreUpdate(int index) => preUpdateItems?.ElementAtOrDefault(index - 1);

    private sealed class DeserializedConfig {
        public double OSVersion { get; init; }
        public int SPVersion { get; init; }
        public double PackageVersion { get; init; }
        public string? ClientUpdatePath { get; init; }
        public string? ServerUpdatePath { get; init; }
        public string? PreUpdatePath { get; init; }
    }
}
