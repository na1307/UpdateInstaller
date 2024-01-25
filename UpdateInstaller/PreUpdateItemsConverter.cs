using Newtonsoft.Json.Linq;

namespace UpdateInstaller;

public sealed class PreUpdateItemsConverter : JsonCreationConverter<PreUpdateItem[]> {
    protected override PreUpdateItem[] Create(Type objectType, JToken jToken) => jToken.Cast<JObject>().Select(t => new PreUpdateItem() { Updates = getUpdates((JArray)t["Updates"]!), Description = t["Description"]!.ToString() }).ToArray();

    private static Update[] getUpdates(JArray array) {
        List<Update> updates = [];

        foreach (var item in array.Cast<JObject>()) {
            if (!EnumHelpers.TryParse(item["Arch"]?.ToString(), out CpuArch arch)) {
                arch = CpuArch.All;
            }

            if (!EnumHelpers.TryParse(item["Platform"]?.ToString(), out OSPlatform platform)) {
                platform = OSPlatform.Both;
            }

            updates.Add(new() {
                FullPath = Path.Combine(ConfigJsonFileHelper.PreUpdatePath + "_" + Arch.ToString(), item["File"]!.ToString() + ".cab"),
                Name = (item["Name"]?.ToString()),
                Arch = arch,
                Platform = platform,
            });
        }

        return [.. updates];
    }
}
