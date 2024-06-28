using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace BlazorDynamics.Common.Parser;
public class SkipEmptyEnumerableConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return typeof(IEnumerable).IsAssignableFrom(objectType);
    }

    public override bool CanWrite
    {
        get { return true; }
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        if (value == null) { writer.WriteNull(); return; }
        var enumerable = value as IEnumerable;
        if (enumerable != null && enumerable.Cast<object>().Any())
        {
            JToken t = JToken.FromObject(value, serializer);
            t.WriteTo(writer);
        }
        else
        {
            writer.WriteNull();
        }
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        JToken token = JToken.Load(reader);
        if (token.Type == JTokenType.Array)
        {
            var elementType = objectType.IsArray ? objectType.GetElementType() : objectType.GetGenericArguments()[0];
            if (elementType == null)
            {
                return existingValue ?? Activator.CreateInstance(objectType);
            }

            var array = token.ToObject(Array.CreateInstance(elementType, 0).GetType(), serializer);
            if (array == null)
            {
                return existingValue ?? Activator.CreateInstance(objectType);
            }

            return typeof(List<>).MakeGenericType(elementType)
                    .GetConstructor([array.GetType()])
                    .Invoke([array]);

        }
        return existingValue ?? Activator.CreateInstance(objectType);
    }

    public override bool CanRead
    {
        get { return true; }
    }
}
