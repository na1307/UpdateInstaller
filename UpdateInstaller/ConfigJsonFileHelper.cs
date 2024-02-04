using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System.Reflection;

namespace UpdateInstaller;

public static class ConfigJsonFileHelper {
    private static readonly string json = File.ReadAllText(ConfigFileName);
    private static readonly JObject jobj = JObject.Parse(json);
    private static readonly DeserializedConfig deserialized = JsonConvert.DeserializeObject<DeserializedConfig>(json)!;

    public static double OSVersion => deserialized.OSVersion;
    public static int SPVersion => deserialized.SPVersion;
    public static double PackageVersion => deserialized.PackageVersion;
    public static string? ClientUpdatePath => deserialized.ClientUpdatePath;
    public static string? ServerUpdatePath => deserialized.ServerUpdatePath;
    public static string? PreUpdatePath => deserialized.PreUpdatePath;
    public static string? OptionalUpdatePath => deserialized.OptionalUpdatePath;
    public static UpdatePathItem[] UpdatePaths { get; } = JsonConvert.DeserializeObject<UpdatePathItem[]>(jobj[nameof(UpdatePaths)]!.ToString())!;
    public static PreUpdateItem[]? PreUpdates { get; } = JsonConvert.DeserializeObject<PreUpdateItem[]>(jobj[nameof(PreUpdates)]?.ToString() ?? string.Empty, new PreUpdateItemsConverter());
    public static OptionalUpdate[]? OptionalUpdates { get; } = JsonConvert.DeserializeObject<OptionalUpdate[]>(jobj[nameof(OptionalUpdates)]?.ToString() ?? string.Empty, new OptionalUpdatesConverter());

    public static bool IsConfigJsonValid {
        get {
            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("UpdateInstaller.UpdateInstaller.schema.json");
            using StreamReader reader = new StreamReader(stream);

            return jobj.IsValid(JsonSchema.Parse(reader.ReadToEnd()));
        }
    }

    private sealed class DeserializedConfig {
        public double OSVersion { get; init; }
        public int SPVersion { get; init; }
        public double PackageVersion { get; init; }
        public string? ClientUpdatePath { get; init; }
        public string? ServerUpdatePath { get; init; }
        public string? PreUpdatePath { get; init; }
        public string? OptionalUpdatePath { get; init; }
    }
}
