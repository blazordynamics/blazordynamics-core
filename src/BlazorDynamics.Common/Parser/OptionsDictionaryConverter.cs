using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlazorDynamics.Common.Parser;

public class OptionsDictionaryConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        // Check if the type is a Dictionary<object, string>
        return objectType == typeof(Dictionary<object, string>);
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        if (value == null) { writer.WriteNull(); return; }
        var dictionary = (Dictionary<object, string>)value;
        var jObject = new JObject();
        foreach (var kvp in dictionary)
        {
            jObject.Add(kvp.Key.ToString() ?? "unknown", JToken.FromObject(kvp.Value, serializer));
        }
        jObject.WriteTo(writer);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        JObject jObject = JObject.Load(reader);
        var dictionary = new Dictionary<object, string>();
        foreach (var property in jObject.Properties())
        {
            dictionary[property.Name] = property.Value?.ToObject<string>() ?? string.Empty;
        }
        return dictionary;
    }
}

