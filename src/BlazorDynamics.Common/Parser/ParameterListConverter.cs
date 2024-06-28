using BlazorDynamics.Common.Helpers;
using BlazorDynamics.Common.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Dynamic;

namespace BlazorDynamics.Common.Parser
{
    public class ParameterListConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ParameterList);
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if(value == null) { writer.WriteNull(); return; }
            var parameterList = value as ParameterList;
            if (parameterList != null && parameterList.Entries != null && parameterList.Entries.Count != 0)
            {
                var jObject = new JObject();
                foreach (var kvp in parameterList.Entries)
                {
                    if (kvp.Key != null && kvp.Value != null)
                    {

                        jObject.Add(new JProperty(TextHelper.FirstCharToLower(kvp.Key), JToken.FromObject(kvp.Value, serializer)));
                    }
                }
                jObject.WriteTo(writer);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);
            var parameters = new Dictionary<string, object>();

            foreach (var property in jObject.Properties())
            {
                var propertyName = TextHelper.FirstCharToUpper(property.Name);
                if (propertyName == "Options")
                {
                    parameters.Add(propertyName, ConvertJTokenToObject(property.Value));
                }
                else if (propertyName == "DefaultValue")
                {
                    parameters.Add(propertyName, ConvertJTokenToExpando(property.Value) ?? JValue.CreateNull());
                }
                else
                {
                    parameters.Add(propertyName, property?.Value?.ToObject<object>(serializer)?? string.Empty);
                }
            }

            return new ParameterList(parameters);
        }


        public static Dictionary<object, string> ConvertJObjectToDictionary(JObject jObject)
        {
            var result = new Dictionary<object, string>();
            foreach (var property in jObject.Properties())
            {
                result[property.Name] = ConvertJTokenToObject(property.Value)?.ToString() ?? string.Empty;
            }
            return result;
        }

        private static object ConvertJTokenToObject(JToken token)
        {
            switch (token.Type)
            {
                case JTokenType.Object:
                    return ConvertJObjectToDictionary(jObject: (JObject)token);
                case JTokenType.Array:
                    var list = new List<object>();
                    foreach (var item in token.Children())
                    {
                        list.Add(ConvertJTokenToObject(item));
                    }
                    return list;
                default:
                    return token.ToObject<object>() ?? JValue.CreateNull();
            }
        }

        public static object? ConvertJTokenToExpando(JToken? token)
        {
            if(token == null) return null;
            if (token.Type == JTokenType.Object)
            {
                var expando = new ExpandoObject() as IDictionary<string, object>;
                foreach (var property in token.Children<JProperty>())
                {
                    expando.Add(property.Name, ConvertJTokenToExpandoOrValue(property.Value) ?? JValue.CreateNull());
                }
                return expando as ExpandoObject;
            }
            else
            {

                return ConvertJTokenToExpandoOrValue(token);
            }
        }

        private static object? ConvertJTokenToExpandoOrValue(JToken token)
        {
            switch (token.Type)
            {
                case JTokenType.Object:
                    return ConvertJTokenToExpando(token);

                case JTokenType.Array:
                    return token.Children().Select(ConvertJTokenToExpandoOrValue).ToList();

                case JTokenType.Integer:
                    return token.Value<long>();

                case JTokenType.Float:
                    return token.Value<double>();

                case JTokenType.String:
                    return token.Value<string>();

                case JTokenType.Boolean:
                    return token.Value<bool>();

                case JTokenType.Date:
                    return token.Value<DateTime>();

                case JTokenType.Null:
                    return null;

                case JTokenType.Undefined:
                    return null;

                // Add additional cases for other JTokenType if needed

                default:
                    return token.ToString();
            }
        }

    }
}
