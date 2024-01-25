using Newtonsoft.Json.Linq;
using static UpdateInstaller.ConfigJsonFileHelper;

namespace UpdateInstaller;

public sealed class OptionalUpdatesConverter : JsonCreationConverter<OptionalUpdate[]> {
    protected override OptionalUpdate[] Create(Type objectType, JToken jToken) {
        List<OptionalUpdate> updates = [];

        foreach (var item in jToken.Cast<JObject>()) {
            if (!EnumHelpers.TryParse(item["Arch"]?.ToString(), out CpuArch arch)) {
                arch = CpuArch.All;
            }

            if (!EnumHelpers.TryParse(item["Platform"]?.ToString(), out OSPlatform platform)) {
                platform = OSPlatform.Both;
            }

            if (((arch & Arch) == 0) || ((platform & Platform) == 0)) {
                continue;
            }

            updates.Add(new() {
                FullPath = Path.Combine(Path.Combine(OptionalUpdatePath + "_" + Arch.ToString(), platform switch {
                    OSPlatform.Client => ClientUpdatePath,
                    OSPlatform.Server => ServerUpdatePath,
                    OSPlatform.Both => string.Empty,
                    _ => throw new InvalidOperationException(platform.ToString())
                }), item["File"]!.ToString() + ".cab"),
                Name = item["Name"]?.ToString(),
                Description = item["Description"]?.ToString(),
                Arch = arch,
                Platform = platform,
            });
        }

        return [.. updates];
    }
}
