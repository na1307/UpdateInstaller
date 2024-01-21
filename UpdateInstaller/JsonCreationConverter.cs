using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UpdateInstaller;

public abstract class JsonCreationConverter<T> : JsonConverter<T> {
    public sealed override bool CanWrite => false;

    public override T? ReadJson(JsonReader reader, Type objectType, T? existingValue, bool hasExistingValue, JsonSerializer serializer) {
        // Load JObject from stream
        JToken jToken = JToken.Load(reader);

        // Create target object based on JObject
        T target = Create(objectType, jToken);

        // Populate the object properties
        serializer.Populate(jToken.CreateReader(), target!);

        return target;
    }

    public sealed override void WriteJson(JsonWriter writer, T? value, JsonSerializer serializer) => throw new NotSupportedException();

    /// <summary>
    /// Create an instance of objectType, based properties in the JSON object
    /// </summary>
    /// <param name="objectType">type of object expected</param>
    /// <param name="jToken">
    /// contents of JSON object that will be deserialized
    /// </param>
    /// <returns></returns>
    protected abstract T Create(Type objectType, JToken jToken);
}
